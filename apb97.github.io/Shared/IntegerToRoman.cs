using System.ComponentModel.DataAnnotations;

namespace apb97.github.io.Shared
{
    public class IntegerToRoman
    {
        [Range(1, 3999)]
        public int Number { get; set; }
        public string? Roman { get; set; }
    }
}
