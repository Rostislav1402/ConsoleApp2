using System;
using System.Collections.Generic;

namespace CinemaBooking
{
    public class Movie
    {
        public string Title { get; private set; }
        public int Duration { get; private set; }

        public Movie(string title, int duration)
        {
            Title = title;
            Duration = duration;
        }

        public override string ToString()
        {
            return Title + " (" + Duration + " мин.)";
        }
    }

    public class Seat
    {
        public int Number { get; private set; }
        public bool IsBooked { get; private set; }

        public Seat(int number)
        {
            Number = number;
            IsBooked = false;
        }

        public bool Book()
        {
            if (IsBooked)
                return false;

            IsBooked = true;
            return true;
        }
    }

    public class CinemaHall
    {
        public string HallName { get; private set; }
        public List<Seat> Places { get; private set; }

        public CinemaHall(string name, int count)
        {
            HallName = name;
            Places = new List<Seat>();

            for (int i = 1; i <= count; i++)
            {
                Places.Add(new Seat(i));
            }
        }

        public void ShowPlaces()
        {
            Console.WriteLine("\nЗал: " + HallName);
            Console.WriteLine("        ЭКРАН");

            foreach (Seat place in Places)
            {
                if (place.IsBooked)
                    Console.Write("[ X ] ");
                else
                    Console.Write("[" + place.Number + "] ");
            }

            Console.WriteLine("\nX — занятое место");
        }
    }

    public class BookingService
    {
        private List<Movie> films;
        private CinemaHall hall;

        public BookingService(List<Movie> movieList, CinemaHall cinemaHall)
        {
            films = movieList;
            hall = cinemaHall;
        }

        public void Run()
        {
            Console.WriteLine("=== Бронирование билетов ===");

            Movie film = SelectFilm();

            if (film == null)
                return;

            Console.WriteLine("\nВыбран фильм: " + film);

            hall.ShowPlaces();
            BookPlace();

            Console.WriteLine("\nСостояние зала после бронирования:");
            hall.ShowPlaces();

            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }

        private Movie SelectFilm()
        {
            Console.WriteLine("\nСписок фильмов:");

            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + films[i]);
            }

            while (true)
            {
                Console.Write("Введите номер фильма: ");
                int number;

                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number >= 1 && number <= films.Count)
                        return films[number - 1];
                }

                Console.WriteLine("Такого номера нет. Попробуйте ещё раз.");
            }
        }

        private void BookPlace()
        {
            while (true)
            {
                Console.Write("\nНомер места: ");
                int number;

                if (!int.TryParse(Console.ReadLine(), out number)
                    || number < 1 || number > hall.Places.Count)
                {
                    Console.WriteLine("Введите номер существующего места.");
                    continue;
                }

                Seat selected = hall.Places[number - 1];

                if (selected.Book())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Место №" + number + " успешно забронировано!");
                    Console.ResetColor();
                    break;
                }
                else
{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Это место уже занято. Выберите другое.");
                    Console.ResetColor();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> films = new List<Movie>()
            {
                new Movie("Дюна", 155),
                new Movie("Грань будущего", 113),
                new Movie("Марсианин", 144),
                new Movie("Пассажиры", 116),
                new Movie("Первому игроку приготовиться", 140)
            };

            CinemaHall hall = new CinemaHall("Зал № 2", 10);

            // Несколько мест заняты заранее
            hall.Places[1].Book();
            hall.Places[6].Book();

            BookingService service = new BookingService(films, hall);
            service.Run();
        }
    }
}