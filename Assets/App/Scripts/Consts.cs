namespace App.Scripts
{
    public static class Tags
    {
        public const string Weapon = "Weapon";
        public const string Hand = "Hand";
        public const string Player = "Player";
    }

    public class AnimatorParams
    {
        // Триггеры состояния катапульты
        public const string LoadingTrigger = "LoadingTrigger";
        public const string LaunchingTrigger = "LaunchingTrigger";
        public const string DefaultTrigger = "DefaultTrigger";

        // Параметры скорости анимаций катапульты
        public const string LoadingSpeed = "LoadingSpeed";
        public const string LaunchingSpeed = "LaunchingSpeed";
    }
}