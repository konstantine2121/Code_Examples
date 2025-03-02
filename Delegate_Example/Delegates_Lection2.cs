namespace Delegate_Example
{
    class PersonController
    {
        private long _dayCounter = 0;
        private readonly Person _person = new Person();

        public void LiveDay()
        {
            _dayCounter++;
            Console.WriteLine($"День {_dayCounter}:");
            _person.DoMorningActions();
            _person.DoDinnerActions();
            _person.DoEveningActions();
            Console.WriteLine();
        }

        public void BecameUgly()
        {
            _person.BecameUgly();
        }
    }

    class Person
    {
        private Action MorningActions;
        private Action DinnerActions;
        private Action EveningActions;

        public Person()
        {
            MorningActions = TakeBreakfest;
            MorningActions += BrushTeeth;
            
            DinnerActions = TakeDinner;
            EveningActions = TakeSupper;
        }

        #region Morning

        public void DoMorningActions()
        {
            MorningActions();
        }

        private void TakeBreakfest()
        {
            Console.WriteLine("Кушаем завтрак");
        }

        private void BrushTeeth()
        {
            Console.WriteLine("Чистим зубы");
        }

        #endregion Morning

        #region Dinner

        public void DoDinnerActions()
        {
            DinnerActions();
        }

        private void TakeDinner()
        {
            Console.WriteLine("Кушаем обед");
        }

        #endregion Dinner

        #region Evening

        public void DoEveningActions() 
        {
            EveningActions();
        }

        private void TakeSupper()
        {
            Console.WriteLine("Кушаем ужин");
        }

        #endregion Evening

        public void BecameUgly()
        {
            Console.WriteLine("Решили бросить чистить зубы");
            MorningActions -= BrushTeeth;
        }

    }
}
