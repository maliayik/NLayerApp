using AutoMapper;
using Core;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsWitCategory());
        }


        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();

        }


        [HttpPost]

        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", products.CategoryId);

            return View(_mapper.Map<ProductDto>(products));


        }

        [HttpPost]

        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);

        }

        public async Task<IActionResult> Remove(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            await _productService.RemoveAsync(products);

            return RedirectToAction(nameof(Index));

        }


    }
}
