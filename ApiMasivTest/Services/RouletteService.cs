using ApiMasivTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMasivTest.Services
{
    public class RouletteService
    {
        /// <summary>
        /// list type variable that stores the created roulettes and their states
        /// </summary>
        public static List<RouletteModel> _listRoulettes = new List<RouletteModel>();
        /// <summary>
        /// Roulette list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RouletteModel> listRoulette()
        {
            return _listRoulettes;
        }
        /// <summary>
        /// New Roulette Registration
        /// </summary>
        /// <param name="roulette"></param>
        public void createRoulette(RouletteModel roulette)
        {
            _listRoulettes.Add(roulette);
        }
        /// <summary>
        /// Roulette opening
        /// </summary>
        /// <param name="idRoulette"></param>
        /// <returns></returns>
        public RouletteModel updateStateRouletteOpen(int idRoulette)
        {
            var roulette = _listRoulettes.FirstOrDefault(rl => rl.Id == idRoulette);
            if(roulette != null && roulette.IsOpenRulette == false)
            {
                roulette.IsOpenRulette = true;
                return roulette;
            }
            else
            {
                Console.WriteLine("No es posible abrir la ruleta");
                return null;
            }
        }
        /// <summary>
        /// Roulette close
        /// </summary>
        /// <param name="idRoulette"></param>
        /// <returns></returns>
        public RouletteModel updateStateRouletteClose(int idRoulette)
        {
            var roulette = _listRoulettes.FirstOrDefault(rl => rl.Id == idRoulette);
            if (roulette != null && roulette.IsOpenRulette == true)
            {
                roulette.IsOpenRulette = false;
                return roulette;
            }
            else
            {
                Console.WriteLine("No es posible cerrar la ruleta");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static List<BetRoulette> _listBetsRoulettes = new List<BetRoulette>();
        /// <summary>
        /// Registration of new bets
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IdRoulette"></param>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public BetRoulette betRoulette(string UserId, int IdRoulette, int number, double money)
        {
            var rouletteInfo = _listRoulettes.FirstOrDefault(rl => rl.Id == IdRoulette);
            var color = colorBet(number);
            if (UserId != null && IdRoulette != null && (money <= 10000 || money >= 1) && (rouletteInfo != null && rouletteInfo.IsOpenRulette == true))
            {
                var list = new BetRoulette();
                _listBetsRoulettes.Add(new BetRoulette() { Id = IdRoulette , number = number, colorBet = color, money = money, IsOpenRulette = true, UserIdBet = UserId});
                return list;
            }
            else
            {
                Console.WriteLine("Error, no se ha podido registrar la apuesta");
                return null;
            }
        }
        /// <summary>
        /// Total bet results
        /// </summary>
        /// <param name="IdRoulette"></param>
        /// <returns></returns>
        public string resultsRoulette(int IdRoulette)
        {
            List<listWinners> _listWinsRoulettes = new List<listWinners>();
            var roulettResults = _listBetsRoulettes.Where(rl => rl.Id == IdRoulette);
            Random _numWin = new Random();
            int numWin = _numWin.Next(0, 36);
            var color = colorBet(numWin);
            var wins = roulettResults.Where(a => a.number == numWin).ToList();
            var lose = roulettResults.Where(a => a.number != numWin).ToList();
            string messageWins = string.Empty;
            string messagelose = string.Empty;
            if (roulettResults != null)
            {
                foreach (BetRoulette number in wins)
                {
                    string prizeNumber = prizeBetNumbre(number.money);
                    string prizeColor = prizeBetColor(number.money);
                    string _message = string.Format("El usuario: " + number.UserIdBet + " ha ganado, por numero ha ganado " + prizeNumber + ", por color " + prizeColor + "{0}", Environment.NewLine);
                    _listWinsRoulettes.Add(new listWinners() { message = _message });
                    messageWins += _message;
                };
                foreach (BetRoulette number in lose)
                {
                    string prizeNumber = prizeBetNumbre(number.money);
                    string prizeColor = prizeBetColor(number.money);
                    string _message = string.Format("El usuario: " + number.UserIdBet + " ha perdido{0}", Environment.NewLine);
                    _listWinsRoulettes.Add(new listWinners() { message = _message });
                    messagelose += _message;
                };
                string finalmessage = string.Format(""+ messageWins + "{0}" + messagelose + "", Environment.NewLine);

                return finalmessage;
            }
            else
            {
                Console.WriteLine("Error, no se han encontrado apuestad");
                return null;
            }
        }
        /// <summary>
        /// Bet color
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string colorBet(int num)
        {
            string color = string.Empty;
            if ((num % 2) == 0)
            {
                color = "Rojo";
            }
            else
            {
                color = "Negro";
            }

            return color;
        }
        /// <summary>
        /// Total prize by number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string prizeBetNumbre(double num)
        {
            double prize = (num * 5);

            return prize.ToString();
        }
        /// <summary>
        /// Total prize for color
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string prizeBetColor(double num)
        {
            double prize = (num * 1.8);

            return prize.ToString();
        }
    }
}
