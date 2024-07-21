using OuvenGameSwordDamage;
namespace Tests1
{
    public class SwordDamageTests
    {
        [Fact]
        public void SwordDamage_Creates_With_Valid_Roll()
        {
            var swordDamage = new SwordDamage(10);
            Assert.NotNull(swordDamage);
        }

        [Fact]
        public void CalculateDamage_NoMagicNoFlame()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Magic = false;
            swordDamage.Flaming = false;
            Assert.Equal(13, swordDamage.Damage); // BASE_DAMAGE + Roll
        }


        [Fact]
        public void CalculateDamage_WithFlame()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Magic = false;
            swordDamage.Flaming = true;
            Assert.Equal(15, swordDamage.Damage); // BASE_DAMAGE + Roll + FLAME_DAMAGE
        }


        [Fact]
        public void CalculateDamage_ChangeRoll()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Roll = 5;
            Assert.Equal(8, swordDamage.Damage); // BASE_DAMAGE + new Roll
        }


        [Fact]
        public void CalculateDamage_EnableFlame()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Flaming = true;
            Assert.Equal(15, swordDamage.Damage); // BASE_DAMAGE + Roll + FLAME_DAMAGE
        }

        [Fact]
        public void CalculateDamage_WithMagic()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Magic = true;
            swordDamage.Flaming = false;
            Assert.Equal(20, swordDamage.Damage); // BASE_DAMAGE + (Roll * 1.75)
        }

        [Fact]
        public void CalculateDamage_WithMagicAndFlame()
        {
            var swordDamage = new SwordDamage(10);
            swordDamage.Magic = true;
            swordDamage.Flaming = true;
            Assert.Equal(22, swordDamage.Damage); // BASE_DAMAGE + (Roll * 1.75) + FLAME_DAMAGE
        }

        [Theory]
        [InlineData(1, 4, 6)]  // Без магии и огня: 4, с магией и огнем: 6
        [InlineData(18, 21, 36)] // Без магии и огня: 21, с магией и огнем: 36
        public void CalculateDamage_BoundaryRollValues(int roll, int expectedDamageWithoutMagicAndFlame, int expectedDamageWithMagicAndFlame)
        {
            var swordDamage = new SwordDamage(roll);

            swordDamage.Magic = false;
            swordDamage.Flaming = false;
            Assert.Equal(expectedDamageWithoutMagicAndFlame, swordDamage.Damage);

            swordDamage.Magic = true;
            swordDamage.Flaming = true;
            Assert.Equal(expectedDamageWithMagicAndFlame, swordDamage.Damage);
        }

        [Fact]
        public void CalculateDamage_ChangeMagicAndFlameAfterCreation()
        {
            var swordDamage = new SwordDamage(10);
            Assert.Equal(13, swordDamage.Damage); // BASE_DAMAGE + Roll
            
            swordDamage.Magic = true;
            Assert.Equal(20, swordDamage.Damage); // BASE_DAMAGE + (Roll * 1.75)

            swordDamage.Flaming = true;
            Assert.Equal(22, swordDamage.Damage); // BASE_DAMAGE + (Roll * 1.75) + FLAME_DAMAGE

            swordDamage.Flaming = false;
            Assert.Equal(20, swordDamage.Damage); // BASE_DAMAGE + (Roll * 1.75)

            swordDamage.Magic = false;
            Assert.Equal(13, swordDamage.Damage); // BASE_DAMAGE + Roll
        }

        [Theory]
        [InlineData(0, 3)]  // Ожидаемый урон при значении Roll = 0
        [InlineData(19, 22)] // Ожидаемый урон при значении Roll = 19 (если такое значение допустимо)
        public void CalculateDamage_InvalidRollValues(int roll, int expectedDamage)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SwordDamage(roll));
        }
    }
}