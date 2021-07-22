using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMasivTest.Models
{
    /// <summary>
    /// Roulette Object
    /// </summary>
    public class RouletteModel
    {
        public int Id { get; set; }
        public bool IsOpenRulette { get; set; } = false;

    }
    /// <summary>
    /// Roulette bet object
    /// </summary>
    public class BetRoulette
    {
        public int Id { get; set; }
        public bool IsOpenRulette { get; set; } = true;
        [Range(0, 36)]
        public int number { get; set; }
        [Range(minimum: 1, maximum: 10000)]
        public double money { get; set; }
        public string colorBet { get; set; }
        public string UserIdBet { get; set; }
    }
    /// <summary>
    /// Roulette results object
    /// </summary>
    public class listWinners
    {
        public string message { get; set; }
    }
}
