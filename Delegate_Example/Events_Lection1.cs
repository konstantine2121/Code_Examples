namespace Evensts_Case1
{
    class AppleCaseRunner
    {
        public void Run()
        {
            Apple apple = new Apple() { Color = ConsoleColor.Green };
            AppleObserver appleObserver = new AppleObserver();
            appleObserver.RegisterColorObserving(apple);

            var stepSleep = 500;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(stepSleep);

                if (i == 9)
                {
                    apple.Color = ConsoleColor.Red;
                }
            }

        }
    }

    class Apple
    {
        private ConsoleColor _color;

        public ConsoleColor Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                _color = value;

                ColorChanged?.Invoke(_color);
            }
        }

        public event Action<ConsoleColor> ColorChanged;

        protected void NotifyColorChanged()
            //Т.к. наследник не может сам вызвать событие
            //ColorChanged?.Invoke(_color);  
            //то приходится писать вот такие функции
        {
            ColorChanged?.Invoke(Color);
        }
    }

    class Semerenko : Apple
    {
        public void TryRaiseColorChanged()
        {
            NotifyColorChanged();
        }
    }

    class AppleObserver
    {
        public void RegisterColorObserving(Apple apple)
        {
            apple.ColorChanged += OnColorChanged; // Подписка на событие
            
            //apple.ColorChanged -= OnColorChanged; //Отписка от события
        }

        private void OnColorChanged(ConsoleColor color)
        {
            Console.WriteLine("Яблоко изменило цвет на " + color);
        }
    }

}


namespace Evensts_Case2
{
    class AppleCaseRunner
    {
        public void Run()
        {
            Apple apple1 = new Apple() { Color = ConsoleColor.Green };
            Apple apple2 = new Apple() { Color = ConsoleColor.Green };
            Apple apple3 = new Apple() { Color = ConsoleColor.Green };
            Apple apple4 = new Apple() { Color = ConsoleColor.Green };
            Apple apple5 = new Apple() { Color = ConsoleColor.Green };
            Apple apple6 = new Apple() { Color = ConsoleColor.Green };
            Apple apple7 = new Apple() { Color = ConsoleColor.Green };
            Apple apple8 = new Apple() { Color = ConsoleColor.Green };
            Apple apple9 = new Apple() { Color = ConsoleColor.Green };
            Apple apple10 = new Apple() { Color = ConsoleColor.Green };

            AppleObserver appleObserver = new AppleObserver();

            appleObserver.RegisterColorObserving(apple1);
            appleObserver.RegisterColorObserving(apple2);
            appleObserver.RegisterColorObserving(apple3);
            appleObserver.RegisterColorObserving(apple4);
            appleObserver.RegisterColorObserving(apple5);
            appleObserver.RegisterColorObserving(apple6);
            appleObserver.RegisterColorObserving(apple7);
            appleObserver.RegisterColorObserving(apple8);
            appleObserver.RegisterColorObserving(apple9);
            appleObserver.RegisterColorObserving(apple10);

            var stepSleep = 500;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(stepSleep);

                if (i == 9)
                {
                    apple3.Color = ConsoleColor.Red;
                }
            }

        }
    }

    class Apple
    {
        private ConsoleColor _color;

        public Apple() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public ConsoleColor Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                _color = value;

                ColorChanged?.Invoke(this, _color);
            }
        }

        //public event Action<ConsoleColor> ColorChanged;
        public event EventHandler<ConsoleColor> ColorChanged;

        protected void NotifyColorChanged()
        //Т.к. наследник не может сам вызвать событие
        //ColorChanged?.Invoke(_color);  
        //то приходится писать вот такие функции
        {
            ColorChanged?.Invoke(this, Color);
        }
    }

    class Semerenko : Apple
    {
        public void TryRaiseColorChanged()
        {
            NotifyColorChanged();
        }
    }

    class AppleObserver
    {
        public void RegisterColorObserving(Apple apple)
        {
            apple.ColorChanged += OnColorChanged; // Подписка на событие

            //apple.ColorChanged -= OnColorChanged; //Отписка от события
        }

        private void OnColorChanged(object? sender, ConsoleColor color)
        {
            Console.WriteLine($"Яблоко {((Apple)sender).Id} изменило цвет на " + color);
        }

    }

}



namespace Evensts_Case3
{
    class AppleCaseRunner
    {
        public void Run()
        {
            Apple apple1 = new Apple() { Color = ConsoleColor.Green };
            Apple apple2 = new Apple() { Color = ConsoleColor.Green };
            Apple apple3 = new Apple() { Color = ConsoleColor.Green };
            Apple apple4 = new Apple() { Color = ConsoleColor.Green };
            Apple apple5 = new Apple() { Color = ConsoleColor.Green };
            Apple apple6 = new Apple() { Color = ConsoleColor.Green };
            Apple apple7 = new Apple() { Color = ConsoleColor.Green };
            Apple apple8 = new Apple() { Color = ConsoleColor.Green };
            Apple apple9 = new Apple() { Color = ConsoleColor.Green };
            Apple apple10 = new Apple() { Color = ConsoleColor.Green };

            AppleObserver appleObserver = new AppleObserver();

            appleObserver.RegisterColorObserving(apple1);
            appleObserver.RegisterColorObserving(apple2);
            appleObserver.RegisterColorObserving(apple3);
            appleObserver.RegisterColorObserving(apple4);
            appleObserver.RegisterColorObserving(apple5);
            appleObserver.RegisterColorObserving(apple6);
            appleObserver.RegisterColorObserving(apple7);
            appleObserver.RegisterColorObserving(apple8);
            appleObserver.RegisterColorObserving(apple9);
            appleObserver.RegisterColorObserving(apple10);

            var stepSleep = 500;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(stepSleep);

                if (i == 9)
                {
                    apple3.Color = ConsoleColor.Red;
                }
            }

        }
    }

    class ColorChangedEventArgs : EventArgs
    {
        public ColorChangedEventArgs(ConsoleColor newColor)
        {
            NewColor = newColor;
        }

        public ConsoleColor NewColor { get; }
    }

    class Apple
    {
        private ConsoleColor _color;

        public Apple()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public ConsoleColor Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                _color = value;

                ColorChanged?.Invoke(this, new ColorChangedEventArgs(_color));
            }
        }

        public event EventHandler<ColorChangedEventArgs> ColorChanged;

        protected void NotifyColorChanged()
        //Т.к. наследник не может сам вызвать событие
        //ColorChanged?.Invoke(_color);  
        //то приходится писать вот такие функции
        {
            ColorChanged?.Invoke(this, new ColorChangedEventArgs(Color));
        }
    }

    class Semerenko : Apple
    {
        public void TryRaiseColorChanged()
        {
            NotifyColorChanged();
        }
    }

    class AppleObserver
    {
        public void RegisterColorObserving(Apple apple)
        {
            apple.ColorChanged += OnColorChanged; // Подписка на событие
        }

        private void OnColorChanged(object? sender, ColorChangedEventArgs e)
        {
            Console.WriteLine($"Яблоко {((Apple)sender).Id} изменило цвет на " + e.NewColor);
        }
    }
}
