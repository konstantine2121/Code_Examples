namespace Delegate_Example
{
    //Fluent Builder
    //https://metanit.com/sharp/patterns/6.1.php

    public class FluentBuilderUsage
    {
        public void Run()
        {
            var Tom = User.CreateBuilder().SetName("Tom").Build();
            var Jerry = User.CreateBuilder().SetName("Jerry").SetCompany("Mouse").Build();
            var Dog = User.CreateBuilder().SetName("Dog").SetCompany("Hpuse keeper").SetAge(5).Build();

            IUserBuilder builder = User.CreateBuilder();
        }
    }

    public interface IUserBuilder
    {
        IUserBuilder IsMarried { get; }

        IUserBuilder SetAge(int age);
        IUserBuilder SetCompany(string company);
        IUserBuilder SetName(string name);

        User Build();
    }

    public class User
    {
        private int _age;

        public string Name { get; set; }        // имя
        public string Company { get; set; }     // компания
        public int Age { get => _age;
            set 
            { 
                if (value > 1000) throw new InvalidOperationException("Таких долгожителей не бывает");
                if (value < 0) throw new InvalidOperationException("Продолжительность жизние может быть отрицательной");
                _age = value; 
            } 
        }            // возраст
        public bool IsMarried { get; set; }      // женат/замужем

        public static IUserBuilder CreateBuilder()
        {
            return new UserBuilder();
        }

        private class UserBuilder : IUserBuilder
        {
            private User user;
            public UserBuilder()
            {
                user = new User();
            }
            public IUserBuilder SetName(string name)
            {
                user.Name = name;
                return this;
            }
            public IUserBuilder SetCompany(string company)
            {
                user.Company = company;
                return this;
            }
            public IUserBuilder SetAge(int age)
            {
                user.Age = age > 0 ? age : 0;
                return this;
            }
            public IUserBuilder IsMarried
            {
                get
                {
                    user.IsMarried = true;
                    return this;
                }
            }
            public User Build()
            {
                return user;
            }
        }
    }
}
