using System;

class Computer
{
    public string Processor;
    public string VideoCard;
    public int Ram;
    public int Storage;
    public string Motherboard;
    public string Case;
    public string PowerSupply;
    public string OperatingSystem;

    public void ShowInfo()
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      КОНФИГУРАЦИЯ КОМПЬЮТЕРА         ║");
        Console.WriteLine("╠══════════════════════════════════════╣");
        Console.WriteLine($"║ Процессор:    {Processor,-22}║");
        Console.WriteLine($"║ Видеокарта:   {VideoCard,-22}║");
        Console.WriteLine($"║ ОЗУ:          {Ram + " GB",-22}║");
        Console.WriteLine($"║ Диск:         {Storage + " TB",-22}║");
        Console.WriteLine($"║ Мат. плата:   {Motherboard,-22}║");
        Console.WriteLine($"║ Корпус:       {Case,-22}║");
        Console.WriteLine($"║ БП:           {PowerSupply,-22}║");
        Console.WriteLine($"║ ОС:           {OperatingSystem,-22}║");
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
}

class ComputerBuilder
{
    private Computer computer = new Computer();

    public ComputerBuilder SetVideoCard(string VideoCard)
    {
        computer.VideoCard = VideoCard;
        return this;
    }
    public ComputerBuilder SetRam(int Ram)
    {
        computer.Ram = Ram;
        return this;
    }
    public ComputerBuilder SetStorage(int Storage)
    {
        computer.Storage = Storage;
        return this;
    }
    public ComputerBuilder SetProcessor(string Processor)
    {
        computer.Processor = Processor;
        return this;
    }
    public ComputerBuilder SetMotherboard(string Motherboard)
    {
        computer.Motherboard = Motherboard;
        return this;
    }
    public ComputerBuilder SetCase(string Case)
    {
        computer.Case = Case;
        return this;
    }
    public ComputerBuilder SetPowerSupply(string PowerSupply)
    {
        computer.PowerSupply = PowerSupply;
        return this;
    }
    public ComputerBuilder SetOperatingSystem(string OperatingSystem)
    {
        computer.OperatingSystem = OperatingSystem;
        return this;
    }

    public Computer Build()
    {
        return computer;
    }
}


class ComputerDirector : ComputerBuilder
{
    private readonly ComputerBuilder builder;

    public ComputerDirector(ComputerBuilder builder)
    {
        this.builder = builder;
    }

    public Computer CreateGamingComputer()
    {
        return builder
            .SetProcessor("Intel Core i9-14900K")
            .SetVideoCard("NVIDIA RTX 4090 24GB")
            .SetRam(32)
            .SetStorage(2)
            .SetMotherboard("ASUS ROG Maximus Z790")
            .SetCase("Игровой корпус с RGB")
            .SetPowerSupply("1200W 80+ Platinum")
            .SetOperatingSystem("Windows 11 Pro")
            .Build();
    }

    public Computer CreateOfficeComputer()
    {
        return builder
            .SetProcessor("Intel Core i5-13400")
            .SetVideoCard("Встроенная Intel UHD 730")
            .SetRam(16)
            .SetStorage(1)
            .SetMotherboard("MSI PRO B760M")
            .SetCase("Офисный корпус Mini-Tower")
            .SetPowerSupply("500W 80+ Bronze")
            .SetOperatingSystem("Windows 11")
            .Build();
    }

    public Computer CreateBudgetComputer()
    {
        return builder
            .SetProcessor("Intel Core i3-12100")
            .SetVideoCard("Встроенная Intel UHD 730")
            .SetRam(8)
            .SetStorage(1)
            .SetMotherboard("H610M")
            .SetCase("Корпус Mini-Tower")
            .SetPowerSupply("400W")
            .SetOperatingSystem("Windows 10")
            .Build();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== СБОРКА КОМПЬЮТЕРА ===");
            Console.WriteLine();
            Console.WriteLine("1 — Игровой компьютер");
            Console.WriteLine("2 — Офисный компьютер");
            Console.WriteLine("3 — Бюджетный компьютер");
            Console.WriteLine("4 — Собрать самостоятельно");
            Console.WriteLine("0 — Выход");
            Console.WriteLine();
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            Computer computer = null;

            switch (choice)
            {
                case "0":
                    return;

                case "1":
                case "2":
                case "3":
                    var builder = new ComputerBuilder();
                    var director = new ComputerDirector(builder);

                    if (choice == "1") computer = director.CreateGamingComputer();
                    else if (choice == "2") computer = director.CreateOfficeComputer();
                    else computer = director.CreateBudgetComputer();
                    break;

                case "4":
                    computer = BuildCustomComputer();
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Нажмите Enter...");
                    Console.ReadLine();
                    continue;
            }

            if (computer != null)
            {
                Console.Clear();
                computer.ShowInfo();
                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }
    }

    static Computer BuildCustomComputer()
    {
        Console.Clear();
        Console.WriteLine("=== САМОСТОЯТЕЛЬНАЯ СБОРКА ===\n");

        var builder = new ComputerBuilder();

        Console.Write("Процессор: ");
        builder.SetProcessor(Console.ReadLine());

        Console.Write("Видеокарта: ");
        builder.SetVideoCard(Console.ReadLine());

        Console.Write("Объём ОЗУ (GB): ");
        int ram;
        while (!int.TryParse(Console.ReadLine(), out ram) || ram <= 0)
            Console.Write("Введите корректное число: ");
        builder.SetRam(ram);

        Console.Write("Объём диска (TB): ");
        int storage;
        while (!int.TryParse(Console.ReadLine(), out storage) || storage <= 0)
            Console.Write("Введите корректное число: ");
        builder.SetStorage(storage);

        Console.Write("Материнская плата: ");
        builder.SetMotherboard(Console.ReadLine());

        Console.Write("Корпус: ");
        builder.SetCase(Console.ReadLine());

        Console.Write("Блок питания: ");
        builder.SetPowerSupply(Console.ReadLine());

        Console.Write("Операционная система: ");
        builder.SetOperatingSystem(Console.ReadLine());

        return builder.Build();
    }
}