using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();
       
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                // LINQ Object
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound(); //404
                }
               
                return Ok(hangHoa);
            }
            // Nhớ phải write Log
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hanghoa = new HangHoa
            { 
                MaHangHoa= Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia=hangHoaVM.DonGia
            };
            hangHoas.Add(hanghoa);
            return Ok(new
            {
                Success = true,
                Data = hanghoa
            });

        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound(); //404
                }
                if (id != hangHoaEdit.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                //Update
                hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            

        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                // LINQ Object
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound(); //404
                }
                // Update
                hangHoas.Remove(hangHoa);
                return Ok(hangHoa);
            }
            // Nhớ phải write Log
            catch
            {
                return BadRequest();
            }

        }
    }
}
