using System;

namespace OuvenGameSwordDamage
{
    public class SwordDamage
    {
        private const int BASE_DAMAGE = 3;
        private const int FLAME_DAMAGE = 2;
        private const int MIN_ROLL = 1;
        private const int MAX_ROLL = 18;
        /// <summary>
        /// Contains the calculated damage.
        /// </summary>
        public int Damage { get; private set; }

        private int roll;
        /// <summary>
        /// Sets or gets the 3d6 roll.
        /// </summary>
        public int Roll
        {
            get { return roll; }
            set
            {
                if (value < MIN_ROLL || value > MAX_ROLL)
                {
                    throw new ArgumentOutOfRangeException(nameof(Roll), "Roll must be between 1 and 18.");
                }
                roll = value;
                CalculateDamage();
            }
        }

        private bool flaming;
        /// <summary>
        /// True, если меч огненный; false в противном случае.
        /// </summary>
        public bool Flaming
        {
            get { return flaming; }
            set { flaming = value; CalculateDamage(); }
        }

        private bool magic;
        /// <summary>
        /// True, если меч волшебный; false в противном случае.
        /// </summary>
        public bool Magic
        {
            get { return magic; }
            set { magic = value; CalculateDamage(); }
        }

        /// <summary>
        /// вычисляет повреждения в зависимости от текущих значений свойств.
        /// </summary>
        private void CalculateDamage()
        {
            decimal magicMultiplier = Magic ? 1.75M : 1M;
            Damage = BASE_DAMAGE + (int)(Roll * magicMultiplier);
            if (flaming) Damage += FLAME_DAMAGE;
        }
        /// <summary>
        /// Конструктор вычисляет повреждения для значений Magic и Flaming по умолчанию 
        /// и начального броска 3d6.
        /// </summary>
        /// <param name="startingRoll">начальныйы бросок 3d6</param>
        public SwordDamage(int startingRoll)
        {
            if (startingRoll < MIN_ROLL || startingRoll > MAX_ROLL)
            {
                throw new ArgumentOutOfRangeException(nameof(startingRoll), "StartingRoll must be between 1 and 18.");
            }
            roll = startingRoll;
            CalculateDamage();
        }
    }
}
