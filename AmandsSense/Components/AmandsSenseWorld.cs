using AmandsSense.Enums;
using AmandsSense.Helpers;
using EFT;
using EFT.Interactive;
using System.Threading.Tasks;
using UnityEngine;

namespace AmandsSense.Components
{
    public class AmandsSenseWorld : MonoBehaviour
    {
        public bool Lazy = true;
        public SenseWorldType SenseWorldType = SenseWorldType.Item;
        public GameObject OwnerGameObject;
        public Collider OwnerCollider;

        public LocalPlayer SenseDeadPlayer;

        public int Id;

        public float Delay;
        public float LifeSpan;

        public bool Waiting = false;
        public bool WaitingRemoveSense = false;
        public bool UpdateIntensity = false;
        public bool Starting = true;
        public float Intensity = 0f;

        private GameObject amandsSenseConstructorGameObject;
        private AmandsSenseConstructor amandsSenseConstructor;

        public void Start()
        {
            enabled = false;
            WaitAndStart();
        }
        private async void WaitAndStart()
        {
            Waiting = true;
            await Task.Delay((int)(Delay * 1000));
            if (WaitingRemoveSense)
            {
                RemoveSense();
                return;
            }

            if (OwnerGameObject == null || (OwnerGameObject != null & !OwnerGameObject.activeSelf))
            {
                RemoveSense();
                return;
            }
            if (Starting)
            {
                if (OwnerGameObject != null)
                {
                    transform.position = OwnerGameObject.transform.position;
                }
                if (HeightCheck())
                {
                    RemoveSense();
                    return;
                }

                enabled = true;
                UpdateIntensity = true;

                amandsSenseConstructorGameObject = new GameObject("Constructor");
                amandsSenseConstructorGameObject.transform.SetParent(gameObject.transform, false);
                amandsSenseConstructorGameObject.transform.localScale = Vector3.one * Settings.Size.Value;

                if (Lazy)
                {
                    ObservedLootItem observedLootItem = OwnerGameObject.GetComponent<ObservedLootItem>();
                    if (observedLootItem != null)
                    {
                        SenseWorldType = SenseWorldType.Item;
                        amandsSenseConstructor = amandsSenseConstructorGameObject.AddComponent<AmandsSenseItem>();
                        amandsSenseConstructor.amandsSenseWorld = this;
                        amandsSenseConstructor.Construct();
                        amandsSenseConstructor.SetSense(observedLootItem);
                    }
                    else
                    {
                        LootableContainer lootableContainer = OwnerGameObject.GetComponent<LootableContainer>();
                        if (lootableContainer != null)
                        {
                            if (lootableContainer.Template == "578f87b7245977356274f2cd")
                            {
                                SenseWorldType = SenseWorldType.Drawer;
                                amandsSenseConstructorGameObject.transform.localPosition = new Vector3(-0.08f, 0.05f, 0);
                                amandsSenseConstructorGameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
                            }
                            else
                            {
                                SenseWorldType = SenseWorldType.Container;
                            }

                            amandsSenseConstructor = amandsSenseConstructorGameObject.AddComponent<AmandsSenseContainer>();
                            amandsSenseConstructor.amandsSenseWorld = this;
                            amandsSenseConstructor.Construct();
                            amandsSenseConstructor.SetSense(lootableContainer);
                        }
                        else
                        {
                            RemoveSense();
                            return;
                        }
                    }
                }
                else
                {
                    switch (SenseWorldType)
                    {
                        case SenseWorldType.Item:
                            break;
                        case SenseWorldType.Container:
                            break;
                        case SenseWorldType.Drawer:
                            break;
                        case SenseWorldType.Deadbody:
                            amandsSenseConstructor = amandsSenseConstructorGameObject.AddComponent<AmandsSenseDeadPlayer>();
                            amandsSenseConstructor.amandsSenseWorld = this;
                            amandsSenseConstructor.Construct();
                            amandsSenseConstructor.SetSense(SenseDeadPlayer);
                            break;
                    }
                }

                // SenseWorld Starting Posittion
                switch (SenseWorldType)
                {
                    case SenseWorldType.Item:
                    case SenseWorldType.Container:
                        gameObject.transform.position = new Vector3(OwnerCollider.bounds.center.x, OwnerCollider.ClosestPoint(OwnerCollider.bounds.center + (Vector3.up * 10f)).y + Settings.VerticalOffset.Value, OwnerCollider.bounds.center.z);
                        break;
                    case SenseWorldType.Drawer:
                        if (OwnerCollider != null)
                        {
                            BoxCollider boxCollider = OwnerCollider as BoxCollider;
                            if (boxCollider != null)
                            {
                                Vector3 position = OwnerCollider.transform.TransformPoint(boxCollider.center);
                                gameObject.transform.position = position;
                                gameObject.transform.rotation = OwnerCollider.transform.rotation;
                            }
                        }
                        break;
                    case SenseWorldType.Deadbody:
                        if (amandsSenseConstructor != null)
                        {
                            amandsSenseConstructor.UpdateSenseLocation();
                        }
                        break;
                }
            }
            else
            {
                LifeSpan = 0f;

                if (HeightCheck())
                {
                    RemoveSense();
                    return;
                }


                if (amandsSenseConstructor != null) amandsSenseConstructor.UpdateSense();

                // SenseWorld Position
                switch (SenseWorldType)
                {
                    case SenseWorldType.Item:
                        gameObject.transform.position = new Vector3(OwnerCollider.bounds.center.x, OwnerCollider.ClosestPoint(OwnerCollider.bounds.center + (Vector3.up * 10f)).y + Settings.VerticalOffset.Value, OwnerCollider.bounds.center.z);
                        break;
                    case SenseWorldType.Container:
                        break;
                    case SenseWorldType.Deadbody:
                        if (amandsSenseConstructor != null) amandsSenseConstructor.UpdateSenseLocation();
                        break;
                    case SenseWorldType.Drawer:
                        break;
                }
            }

            Waiting = false;
        }
        public void RestartSense()
        {
            if (Waiting || UpdateIntensity) return;

            LifeSpan = 0f;
            Delay = Vector3.Distance(AmandsSenseClass.Player.Position, gameObject.transform.position) / Settings.Speed.Value;
            WaitAndStart();
        }
        public bool HeightCheck()
        {
            switch (SenseWorldType)
            {
                case SenseWorldType.Item:
                case SenseWorldType.Container:
                case SenseWorldType.Drawer:
                case SenseWorldType.Deadbody:
                    return AmandsSenseClass.Player != null && (transform.position.y < AmandsSenseClass.Player.Position.y + Settings.MinHeight.Value || transform.position.y > AmandsSenseClass.Player.Position.y + Settings.MaxHeight.Value);
            }
            return false;
        }
        public void RemoveSense()
        {
            if (amandsSenseConstructor != null) amandsSenseConstructor.RemoveSense();
            AmandsSenseClass.SenseWorlds.Remove(Id);
            if (gameObject != null) Destroy(gameObject);
        }
        public void CancelSense()
        {
            UpdateIntensity = true;
            Starting = false;
        }
        public void Update()
        {
            if (UpdateIntensity)
            {
                if (Starting)
                {
                    Intensity += Settings.IntensitySpeed.Value * Time.deltaTime;
                    if (Intensity >= 1f)
                    {
                        UpdateIntensity = false;
                        Starting = false;
                    }
                }
                else
                {
                    Intensity -= Settings.IntensitySpeed.Value * Time.deltaTime;
                    if (Intensity <= 0f)
                    {
                        if (Waiting)
                        {
                            WaitingRemoveSense = true;
                        }
                        else
                        {
                            RemoveSense();
                        }
                        return;
                    }
                }

                if (amandsSenseConstructor != null) amandsSenseConstructor.UpdateIntensity(Intensity);

            }
            else if (!Starting && !Waiting)
            {
                LifeSpan += Time.deltaTime;
                if (LifeSpan > Settings.Duration.Value)
                {
                    UpdateIntensity = true;
                }
            }
            if (Camera.main != null)
            {
                switch (SenseWorldType)
                {
                    case SenseWorldType.Item:
                    case SenseWorldType.Container:
                    case SenseWorldType.Deadbody:
                        transform.rotation = Camera.main.transform.rotation;
                        transform.localScale = Vector3.one * Mathf.Min(Settings.SizeClamp.Value, Vector3.Distance(Camera.main.transform.position, transform.position));
                        break;
                    case SenseWorldType.Drawer:
                        break;
                }
            }
        }
    }
}
