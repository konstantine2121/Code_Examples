using System.Net.Http.Headers;

namespace Basket_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Fruit pear = new Fruit(Guid.NewGuid(),"Груша", 1232.35434);
            Apple apple = new Apple(Guid.NewGuid(), 1234.34, "Папировка");


            
            PrintGood(pear);
            PrintGood(apple);
            #region Comment
            /*
                        Apple apple = new Apple();
                        Fruit fruit = new Fruit();
                        Fruit appleFruit = apple;
                        BaseGood baseApple= apple;


                        PrintGood(apple);
                        PrintGood(appleFruit);
                        PrintGood(baseApple);
                        PrintGood(fruit);
            */
            #endregion Comment
        }

        static void PrintGood(IGood good)
        {
            Console.WriteLine($"Имя: {good.Name}");
        }

        static void PrintGood(Apple good)
        {
            PrintGood((IGood)good);

            Console.WriteLine($"Сорт: {good.AppleSort}");
        }
    }

    #region IGood

    public interface IGood
    {
        string Name { get; }

        Guid Id { get; }

        double Cost { get; }
    }

    public abstract class BaseGood : IGood
    {
        protected BaseGood(Guid id, double cost) 
        {
            Id = id;

            if (cost < 0)
            {
                throw new InvalidOperationException($"'{nameof(cost)}' = {cost}. Стоимость товара не может быть отрицательной");
            }

            Cost = cost;
        }

        public abstract string Name { get; }

        //Полная запись
        public Guid Id { get; }

        public double Cost { get; }
    }

    public class Fruit : BaseGood
    {
        /// <summary>
        /// Id задается автоматом
        /// </summary>
        public Fruit(double cost) : base(Guid.NewGuid(), cost)
        {
        }

        /// <summary>
        /// Id необходимо передать снаружи
        /// </summary>
        /// <param name="id"></param>
        public Fruit(Guid id, double cost) : base(id, cost)
        {

        }

        /// <summary>
        /// Id необходимо передать снаружи <br/>
        /// Name необходимо передать снаружи
        /// </summary>
        /// <param name="id">Корректный id</param>
        /// <param name="name">Супер имя</param>
        public Fruit(Guid id, string name, double cost) : base(id, cost)
        {
            Name = name;
        }

        public override string Name { get; } = "Фрукт";
    }

    public class Apple : Fruit
    {
        public Apple(double cost) : base(cost)
        {
        }

        /// <summary>
        /// Id необходимо передать снаружи
        /// </summary>
        /// <param name="id"></param>
        public Apple(Guid id, double cost, string sort = null) : base(id, cost)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                AppleSort = sort;
            }
        }

        public override string Name => "Яблоко";

        public string AppleSort { get; } = "Неисвестный сорт";  
        
        public AppleSorts AppleSort2 { get; } = AppleSorts.Грушовка_Московская;        
    }

    public enum AppleSorts
    {
        Белый_налив,
        Грушовка_Московская,
        Конфетное,
        Коробовка,
        Коваленковское,
        Квинти = 10,
        Летнее_полосатое,
        Мантет,
        Медуница,
        Папировка,
        Мельба
    }

    #endregion IGood

    public interface IBasket
    {
        void Add(IGood good);

        void Remove(Guid Id);
        
        IReadOnlyList<IGood> Goods { get; }

        double TotalCost { get; }
    }

    public class Basket : IBasket
    {
        private readonly Dictionary<Guid, IGood> _goods = new Dictionary<Guid, IGood>();

        public IReadOnlyList<IGood> Goods => _goods.Values.ToArray();

        //public double TotalCost => _goods.Values.Sum(good => good.Cost);

        public double TotalCost
        {
            get
            {
                double totalCost = 0;

                foreach (var good in _goods.Values) 
                {
                    totalCost += good.Cost;
                }

                return totalCost;
            }
        }

        public void Add(IGood good)
        {         
            if (good == null)
            {
                throw new ArgumentNullException(nameof(good));
            }

            _goods.Add(good.Id, good);
        }

        public void Remove(Guid Id)
        {
            _goods.Remove(Id);
        }
    }

    public class Customer
    {
        private IBasket Basket { get; }
    }
}
