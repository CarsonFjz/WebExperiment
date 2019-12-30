using System.ComponentModel.DataAnnotations;

namespace WebTest.Model
{
    public class TestRequest
    {
        [Required(ErrorMessage = "商品不得为空")]
        public string Name { get; set; }
    }
}
