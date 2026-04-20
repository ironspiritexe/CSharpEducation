namespace TicTacToe;

class Program
{
    static char [] board = {'1','2','3','4','5','6','7','8','9'}; // игровое поле - массив из 9 ячеек
    static char player = 'X'; // текущий игрок, начинает X
    static bool gameOver = false; // завершение игры

    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в игру Крестики-Нолики! Нажмите Enter что бы продолжить."); // приветствие
        Console.ReadLine();
        string loop; // переменная петли
        do // основной цикл позволяющий играть многократно
        {
            ResetBoard(); // сброс игрового поля и переменных перед новой игрой
            while (!gameOver) // игровой цикл
            {
                GameBoard(); // метод отрисовки игрового поля
                MakeMove(); // метод обработки хода
                CheckforWin(); // метод проверки подеды/ничьей

                if (!gameOver) // смена игрока если игра не закончена
                    player = (player == 'X') ? 'O' : 'X';
            }
            
            Console.WriteLine("Хотите сыграть еще раз? (Y/N)"); // запрос на повторную игру
            loop = Console.ReadLine();

        } while (loop.ToUpper() == "Y");
        
        Console.WriteLine("Спасибо за игру!");
    }

    static void ResetBoard() // метод сброса игрового поля
    {
        board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        gameOver = false;
        player = 'X';
    }

    static void GameBoard() // метод отрисовки игрового поля
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("     " + board[0] + "  |  " + board[1] + "  |  " + board[2]);
        Console.WriteLine("     -------------");
        Console.WriteLine("     " + board[3] + "  |  " + board[4] + "  |  " + board[5]);
        Console.WriteLine("     -------------");
        Console.WriteLine("     " + board[6] + "  |  " + board[7] + "  |  " + board[8]);
        Console.WriteLine();
    }

    static void MakeMove() // метод обработки хода
    {
        Console.WriteLine("Ход игрока " + player + "." + " Введите позицию от 1 до 9");
        if (!int.TryParse(Console.ReadLine(), out int choise)) // преобразование ввода и проверка на корректность
        {
            Console.WriteLine("Ошибка: Нужно ввести число!");
            MakeMove();
            return;
        }

        if (choise < 1 || choise > 9 || board[choise - 1] == 'X' || board[choise - 1] == 'O') // проверка диапазона от 1 до 9 и незанятость клетки
        {
            Console.WriteLine("Неверный ход! Клетка занята или выбранная позиция вне диапазона.");
            MakeMove();
            Console.ReadLine(); // пауза для чтения
        }
        else
        {
            board[choise - 1] = player; // запись символа текущего игрока в выбраную ячейку
        }
    }

    static void CheckforWin() // метод проверки подеды/ничьей
    {
        int[,] win = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, //Горизонтальные
                      {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, //Вертикальные
                      {0, 4, 8}, {2, 4, 6}};           //Диагональные
        for (int i = 0; i < win.GetLength(0); i++) // перебор выигрышных комбинаций
        {
            int a = win[i, 0];
            int b = win[i, 1];
            int c = win[i, 2];

            if (board[a] == board[b] && board[b] == board[c]) // если три ячейки принадлежат одному игроку
            {
                GameBoard();
                Console.WriteLine("Игрок " + board[a] + " победил!");
                gameOver = true;
                return;
            }
        }
        
        int draw = 0; // проверка на ничью
        foreach (char cell in board)
        {
                if (cell == 'X' || cell == 'O') 
                    draw++;
        }
        if (draw == 9)
        {
                GameBoard();
                Console.WriteLine("Ничья!");
                gameOver = true;
        }
    }
    
}