using EFT;

namespace AmandsSense.Models
{
    public struct SenseDeadPlayer
    {
        public Player victim;
        public Player aggressor;

        public SenseDeadPlayer(Player Victim, Player Aggressor)
        {
            victim = Victim;
            aggressor = Aggressor;
        }
    }
}
