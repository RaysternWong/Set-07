using BuyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyGame
{
    /**
     * Buy Game
     *
     * Next month, a popular game company will release new LAN multiplayer game in XCity.
     * A group of student from XHighschool want to buy the new game.
     * 
     * A student CAN buy the game if their available money is enough to buy the game.
     * 
     * Because the game is LAN multiplayer game, a student MAY buy the game
     * if he CAN buy the game and at least 2 of his/her friends CAN buy the game,
     * REGARDLESS those 2 friends MAY buy the game or not.
     * 
     * A student do not consider other student as friend if not exist in his/her friend list.
     * 
     * ================================ ATTENTION PLEASE ================================
     *
     * Your ONLY task is to implement the following two methods:
     * 1. GetListBuyers
     * 2. GetListPromoBuyers
     *
     * You are ALLOWED to
     * 1. Add new method(s) in this file.
     * 2. Add additional test case(s) in Main.
     *
     * You are NOT ALLOWED to
     * 1. Add any new method in other files.
     * 2. Change signature of any existing methods.
     *    Method signature includes
     *    - method's name
     *    - return type
     *    - number of parameters
     *    - parameters' type
     *    - access modifier
     *
     * --- Please make sure your code is error-free when built.
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            var students = _CreateStudents();

            // Set Game Price
            const double GAME_PRICE = 7000;

            Console.WriteLine("Game price: " + GAME_PRICE);
            Console.WriteLine();

            // GetListBuyers
            Console.WriteLine("Students that may buy the game:");
            _CheckAnswer(GetListBuyers(students, GAME_PRICE), _GetStudents(students, _studentMayBuy));

            Console.WriteLine();

            // GetListPromoBuyers
            Console.WriteLine("Students that may buy the game (promo):");
            _CheckAnswer(GetListPromoBuyers(students, GAME_PRICE), _GetStudents(students, _studentMayBuyPromo));

            Console.ReadKey();
        }

        /**
         * GetListBuyers
         *
         * Return list of student that may buy the game.
         * List of student (not null) and game price are given.
         * Note: A student may buy the game if at least 2 of his/her friends can buy the game
         */
        public static List<Student> GetListBuyers(List<Student> students, double gamePrice)
        {
            // AMEND YOUR CODE BELOW

            throw new NotImplementedException();

            // AMEND YOUR CODE ABOVE
        }

        /**
         * GetListPromoBuyers
         *
         * In this case, there is a promo: "Buying 2 game with paying half price for the second game."
         * A student that can buy with full price will invite his/her friend that:
         * - have the fewest money
         * - cannot buy the game with full price
         * - can buy the game with half price
         * - not yet invited to buy the game with promo price by other student
         * to buy the second game with half price.
         * 
         * List of student that given already ordered by who will invite other friend to buy promo,
         * earlier to later.
         * 
         * Return list of student that may buy the game with promo.
         * List of student (not null) and game price are given.
         * Note: A student may buy the game if at least 2 of his/her friends can buy the game
         */
        public static List<Student> GetListPromoBuyers(List<Student> students, double gamePrice)
        {
            // AMEND YOUR CODE BELOW

            throw new NotImplementedException();

            // AMEND YOUR CODE ABOVE
        }

        #region Test Case

        private static List<Student> _CreateStudents()
        {
            var students = new List<Student>();

            var studentA = new Student("Student A", 3500);
            var studentB = new Student("Student B", 9800);
            var studentC = new Student("Student C", 7700);
            var studentD = new Student("Student D", 2000);
            var studentE = new Student("Student E", 700);
            var studentF = new Student("Student F", 6900);
            var studentG = new Student("Student G", 7100);
            var studentH = new Student("Student H", 5200);
            var studentI = new Student("Student I", 4100);
            var studentJ = new Student("Student J", 7300);

            studentA.Friends = new List<Student> { studentD, studentF, studentG, studentH, studentJ };
            studentB.Friends = new List<Student> { studentE, studentG };
            studentC.Friends = new List<Student> { studentB, studentD, studentG, studentH };
            studentD.Friends = new List<Student> { studentC, studentF, studentG, studentH, studentI, studentJ };
            studentE.Friends = new List<Student> { studentC, studentD, studentF, studentJ };
            studentF.Friends = new List<Student> { studentA, studentC, studentD, studentE, studentG, studentJ };
            studentG.Friends = new List<Student> { studentA, studentB, studentC };
            studentH.Friends = new List<Student> { studentA, studentF, studentG, studentI, studentJ };
            studentI.Friends = new List<Student> { studentA, studentB, studentD, studentG, studentH };
            studentJ.Friends = new List<Student> { studentA, studentB, studentE, studentF, studentH, studentI };

            students.Add(studentA);
            students.Add(studentB);
            students.Add(studentC);
            students.Add(studentD);
            students.Add(studentE);
            students.Add(studentF);
            students.Add(studentG);
            students.Add(studentH);
            students.Add(studentI);
            students.Add(studentJ);

            return students;
        }

        private static readonly List<string> _studentMayBuy = new List<string>
        {
            "Student C",
            "Student G"
        };

        private static readonly List<string> _studentMayBuyPromo = new List<string>
        {
            "Student C",
            "Student G",
            "Student J",
            "Student H",
            "Student A",
            "Student I"
        };

        #endregion

        #region Test Helpers

        private static void _CheckAnswer(List<Student> answer, ICollection<Student> expected)
        {
            if (answer == null)
            {
                answer = new List<Student>();
            }

            foreach (var student in answer)
            {
                Console.Write(student.Name);

                Console.WriteLine(expected.Contains(student) ? " (Right)" : " (Wrong)");
            }


            foreach (var student in expected.Except(answer))
            {
                Console.WriteLine($"(Wrong, expected also:{student.Name})");
            }
        }

        private static List<Student> _GetStudents(IEnumerable<Student> students, IEnumerable<string> names)
        {
            return students.Where(student => names.Contains(student.Name)).ToList();
        }

        #endregion
    }
}
