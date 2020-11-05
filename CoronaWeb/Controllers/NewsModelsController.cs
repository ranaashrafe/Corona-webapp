using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoronaWeb.CoronaDb;
using CoronaWeb.Models;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CoronaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public NewsModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsModel>>> GetNewsModels()
        {
            return await _context.NewsModels.Include(obj => obj.NewsSourceModel)
                .Include(obj => obj.NewsSourceModel.CountryModel).ToListAsync();
        }

        // GET: api/NewsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsModel>> GetNewsModel(int id)
        {
            var newsModel = await _context.NewsModels.Where(c => c.Id== id).Include(obj => obj.NewsSourceModel)
                .Include(obj => obj.NewsSourceModel.CountryModel).FirstOrDefaultAsync();

            if (newsModel == null)
            {
            }

            return newsModel;
        }

        // PUT: api/NewsModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsModel(int id, NewsModel newsModel)
        {
            if (id != newsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NewsModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<NewsModel>> PostNewsModel(NewsModel newsModel)
        {
            newsModel.NewsSourceModel.NewsModels = null;
            newsModel.NewsSourceModel.CountryModel.UserModels = null;
            _context.NewsModels.Add(newsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel);
        }

        // DELETE: api/NewsModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NewsModel>> DeleteNewsModel(int id)
        {
            var newsModel = await _context.NewsModels.FindAsync(id);
            if (newsModel == null)
            {
                return NotFound();
            }

            _context.NewsModels.Remove(newsModel);
            await _context.SaveChangesAsync();

            return newsModel;
        }

        private bool NewsModelExists(int id)
        {
            return _context.NewsModels.Any(e => e.Id == id);
        }
        [HttpGet]
        [Route("SearchBySource")]

        public async Task<List<NewsModel>> SearchBySource(string SearchString)
        {
            var NewSource = await _context.NewsModels.Where(x => x.NewsSourceModel.Name.Contains(SearchString)).Include(obj => obj.NewsSourceModel)
                .Include(obj => obj.NewsSourceModel.CountryModel).ToListAsync();

            if (NewSource == null)

            {

            }

            return NewSource;
        }



        [HttpGet]
        [Route("SearchByCountry")]

        public async Task<List<NewsModel>> SearchByCountry(string SearchString)
        {
            var NewSource = await _context.NewsModels.Where(x => x.NewsSourceModel.CountryModel.Name.Contains(SearchString)).Include(obj => obj.NewsSourceModel)
                 .Include(obj => obj.NewsSourceModel.CountryModel).ToListAsync();

            if (NewSource == null)

            {

            }

            return NewSource;
        }
        [HttpGet]
        [Route("BBCNEWS")]
        public  async Task<IActionResult> GetHtmlAsyncBBC()
        {
            var url = "https://www.bbc.com/arabic/search?q=%D9%83%D9%88%D8%B1%D9%88%D9%86%D8%A7";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("ws-search-components")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("div")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Contains("hard-news-unit hard-news-unit--regular faux-block-link")).ToList();
          
            
         
            foreach (var item in ProductListItems)
            {
                
                NewsModel newsModel = new NewsModel();
               
                var editNews = item.InnerText.Trim(' ', '\n', '\r', ' ', '"','/');
                var editNewsSource = editNews.Trim('"').Trim('?');
                string[] lines = Regex.Split(editNewsSource, "\n");
             
              
                
             var Title= lines[0].Trim('/').TrimEnd(' ').TrimStart(' ');
                newsModel.Title = Regex.Replace(Title,"/"," ").Trim(' ');

                      newsModel.Text = lines[1].Trim('/').Trim('"').Trim('?').TrimEnd(' ').TrimStart(' ');
                     newsModel.Date= lines[3].Trim('/').Trim('"').Trim('?').TrimEnd(' ').TrimStart(' ').ToString();
                      newsModel.Detail = null;
                     newsModel.Image = "80327685.png";
                      newsModel.NewsSourceModel = null;
                      newsModel.NewsSourceModelId = 1;
                      

                _context.NewsModels.Add(newsModel);
                CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel);
                // _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[NewsModels] ON");



                //  _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[NewsModels] OFF");
            }
            await _context.SaveChangesAsync();
            return NoContent();
           
        }


        [HttpGet]
        [Route("Kwauit")]
        public async Task<IActionResult> GetHtmlAsyncKwauit()
        {
            var url = "http://www.annaharkw.com/SearchArticles.aspx?text=%D9%83%D9%88%D8%B1%D9%88%D9%86%D8%A7";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("ctl00_ContentPlaceHolder1_Table1")).ToList();
            var ProductListItems = ProductsHtml[0].Descendants("div")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Contains("item-content")).ToList();

           
         
            foreach (var item in ProductListItems)
            {
                NewsModel newsModel = new NewsModel();
              //  var childs = ProductListItems[0].ChildNodes[1];
               
                var editNews = item.InnerText.Trim(' ', '\n', '\r', ' ', '"');
                var editNewsSource = editNews.Trim('"');
                string[] lines = Regex.Split(editNewsSource, "\n");
                var Date= lines[0].TrimEnd(' ', 'r', '/').TrimStart(' ', 'r', '/').ToString();
                newsModel.Date = Regex.Replace(Date, "/r"," ").Trim(' ');
                newsModel.Title = lines[6].TrimEnd(' ').TrimStart(' ');
                newsModel.Text = lines[10].TrimEnd(' ').TrimStart(' ');
                newsModel.Image = "4655154.png";
                newsModel.NewsSourceModel = null;

                newsModel.Detail = null;
                newsModel.NewsSourceModelId = 2;
                _context.NewsModels.Add(newsModel);
                CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel);

            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("Emarat")]
        public async Task<IActionResult> GetHtmlAsyncEmarat()
        {
            var url = "https://www.emaratalyoum.com/search-7.160?s=d&q=%D9%83%D9%88%D8%B1%D9%88%D9%86%D8%A7";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("grid_8 pull_right content")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("li")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Contains("el")).ToList();
            foreach (var item in ProductListItems)
            {


                NewsModel newsModel = new NewsModel();
                var editTitle = item.InnerText.Trim(' ', '\n','&');
                var edittt = editTitle.Trim('&');
                string[] lines = Regex.Split(edittt, "\t\t");
               var  Title = lines[8].Trim('&', '\n', 'q', 'u', 'o', 't', ';').TrimStart(' ').TrimEnd(' ');
                newsModel.Title = Regex.Replace(Title, "&quot;", " ").Trim(' '); ;
                var Text = lines[16].Trim('&', '\n', 'q', 'u', 'o', 't', ';').TrimStart(' ').TrimEnd(' ');
                newsModel.Text = Regex.Replace(Text, "&quot;", " ").Trim(' ');
                newsModel.NewsSourceModel = null;
                newsModel.Detail = null;
                newsModel.NewsSourceModelId = 3;
                newsModel.Date = DateTime.Now.ToString();
                newsModel.Image = "89170909.png";
                _context.NewsModels.Add(newsModel);
                CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("Saudi")]
        public async Task<IActionResult> GetHtmlAsyncSaudi()
        {
            var url = "http://www.alriyadh.com/search/srch?sort=date&q=%D9%83%D9%88%D8%B1%D9%88%D9%86%D8%A7";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("col-lg-9")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("div")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Contains("tr-post bx")).ToList();
            foreach (var item in ProductListItems)
            {


                NewsModel newsModel = new NewsModel();
                var childs = ProductListItems[0].ChildNodes[1];
                newsModel.Image = childs.InnerHtml.Split('"')[1];
                var editTitle = item.InnerText.Trim(' ', '\n');
                var edittt = editTitle.Trim('&');
                string[] lines = Regex.Split(edittt, "\t\t");
                newsModel.Title = lines[6].Trim('&', '\n').TrimStart(' ').TrimEnd(' ');
                newsModel.Text = lines[10].Trim('&', '\n').TrimStart(' ').TrimEnd(' ');
                newsModel.Date = DateTime.Now.ToString();
                newsModel.NewsSourceModel = null;
                newsModel.Detail = null;
                newsModel.NewsSourceModelId = 4;
                newsModel.Image = "98763131.jpg";
                _context.NewsModels.Add(newsModel);
                CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel); 
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("Egypt")]
        public async Task<IActionResult> GetHtmlAsyncEgypt()
        {
            var url = "https://www.youm7.com/Home/Search?allwords=%D9%83%D9%88%D8%B1%D9%88%D9%86%D8%A7";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("frmdata")).ToList();


            var ProductListItems = ProductsHtml[0].Descendants("div")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Contains("info_section news-item")).ToList();
            foreach (var item in ProductListItems)
            {


                NewsModel newsModel = new NewsModel();
                var childs = ProductListItems[0].ChildNodes[1];
                newsModel.Image = childs.InnerHtml.Split('"')[1];
                var editTitle = ProductListItems[0].InnerText.Trim(' ', '\n');
                var editText = ProductListItems[0].InnerText.Trim(' ', '\n');
                newsModel.Title = editTitle.Split('\n')[0]; // ProductListItems[0].InnerText.Trim();
                newsModel.Text = editText.Split(' ')[0];
                newsModel.NewsSourceModel = null;
                newsModel.Date = DateTime.Now.ToString();
                newsModel.Detail = null;
                newsModel.NewsSourceModelId = 1;
                _context.NewsModels.Add(newsModel);
                CreatedAtAction("GetNewsModel", new { id = newsModel.Id }, newsModel);

            }
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
