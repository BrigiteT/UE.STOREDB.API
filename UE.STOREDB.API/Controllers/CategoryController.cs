using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Configuration;
using System.Drawing;
using UE.STOREDB.API.Controllers;
using UE.STOREDB.DOMAIN.Core.DTO;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UE.STOREDB.DOMAIN.Infraestructure.Data;

namespace UE.STOREDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null) { return NotFound(); }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Category category)
        {
            var result = await _categoryRepository.Insert(category);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            if (id != category.Id) { return BadRequest(); }
            var result = await _categoryRepository.Update(category);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryRepository.Delete(id);
            if(!result) return BadRequest();
            return Ok(result);  
        }
    }
}



//POSTMAN
//1. Crear un contenedor
//METODO 01:
//2.En los 3 puntos (...), Add request; en la pantalla derecha:
//   Colocar un nombre al metodo (ejemplo: HTTP GET --> PeopleALL)
//   En GET: colocar la URL, y clic en send
//*****Ir Grabando los cambios*****
//METODO 02:
//2.Importar, copiar el codigo "curl -X" de Swagger
//   Grabarlo y colocar el nombre y a que contenedor va a pertenecer
//3. Grabar todo y Exportar el contenedor.


//PASOS PARA GENERAR ENDPOINT EN VISUAL STUDIO 2022

//Scaffold-DBContext "Server=BRIGITETARA9440;Database=PeruDB;Integrated Security=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -Force -nopluralize
//Scaffold-DBContext "Server=BRIGITETARA9440;Database=PeruDB;User=sa;Pwd=123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -Force -nopluralize

//****************PROYECTO API CON CONTROLADOR AUTOGERENERADO - SCAFFOLDING********************
//1. Crear nuevo proyecto: ASP.NET Core Web API (c#)
//2. Instalar los 4 paquetes desde NuGet Package Manager, Package Manager Console: 
//   Install - Package Microsoft.EntityFrameworkCore
//   Install-Package Microsoft.EntityFrameworkCore.SqlServer
//   Install-Package Microsoft.EntityFrameworkCore.Tools
//   Install-Package Microsoft.EntityFrameworkCore.Design                  
//3. Ejecutar el Scaffold desde NuGet Package Manager, Package Manager Console: 
//   Scaffold - DbContext "Server=BRIGITETARA9440;Database=PeruDB;Integrated Security=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data - Force - nopluralize
//4.Agregar la cadena de conexión en appsettings.json: 
//   , 
//   "ConnectionStrings":{ "DevConnection": "Server=BRIGITETARA9440;Database=PeruDB;Integrated Security=true;TrustServerCertificate=True"}
//5.Agregar la conexión a Program.cs, en //Add services to the container: 
//   var _config = builer.Contiguration;
//var cnx = _config.GetConnectionString("DevConnection");
//builder.Services
//   .AddDbContext<PeruDbContext>(options => options.UseSqlServer(cnx));
//***Compilar la solucion hasta este punto, para verificar que no hayan errores***
//6. Ir a:
//   Controllers, clic derecho, Add, New Scaffolded Item ..., API Controller with actions, using Entity Framework, Add,
//   Para el caso de la tabla People:
//   Model: People, DbContext Class: PeruDbContext, Controller name: PeopleController
//*** Ejecutamos en HTTP, y verificamos en Swagger los EndPoint creados***
//7. Subir a GitHub:
//   Clic derecho a la solución, Create Git repository

//****************PROYECTO API SIN CONTROLADOR AUTOGERENERADO - SIN SCAFFOLDING********************
//1. Crear la solución con el proyecto principal: ASP.NET Core Web API (c#), name: UE.STOREDB.API
//2. Adicionar un nuevo proyecto (biblioteca de clases):
//   Clic derecho a la solución, add, New Project ..., Class Library (c#), add, Project name: UE.STOREDB.DOMAIN, next, create
//   ***Podemos colocar temporalmente esta biblioteca como proyecto predeterminado***
//   ***Clic derecho a este proyecto, Set at Startup Project ***
//   ***Si queremos dejar el otro projecto predeterminado, hacemos lo mismo en el projecto de API***
//3. Dentro del nuevo proyecto de biblioteca:
//   a) Eliminar la clase creada por defecto: class1.cs
//   b) Agregar nuevas carpetas: clic derecho, add, new folder
//      carpetas: Core(DTO, Entities, Interfaces, Services, Settings), 
//	            Infraestructure(Data, Repositories, Shared)
//   c) Instalar los 4 paquetes desde NuGet Package Manager, Package Manager Console, seleccionar en "default project" : "UE.STOREDB.DOMAIN":
//      Install - Package Microsoft.EntityFrameworkCore
//      Install-Package Microsoft.EntityFrameworkCore.SqlServer
//      Install-Package Microsoft.EntityFrameworkCore.Tools
//      Install-Package Microsoft.EntityFrameworkCore.Design 

//   d) Ejecutar el Scaffold desde NuGet Package Manager, Package Manager Console: 
//      Scaffold - DbContext "Server=BRIGITETARA9440;Database=StoreDB;Integrated Security=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Infrastructure / Data - Force - nopluralize
//      - Solo dejar el archivo: StoreDbContext en la carpeta Infrastructure/Data
//	  - Mover las demas entidades a la carpeta Core/Entities (ctrlX y ctrlV)
//   e)En la carpeta Repositories, creamos un repositorio por entidad (usaremos la entidad Category para el ejemplo)
//     Clic derecho en Repositories, add, Class, Name: CategoryRepository.cs
//                    en la clase: CategoryRepository.cs, IMPORTANTE: crear los metodos necesarios(get, getbyid, create, update, delete or deletelogic)
//   f) Crear la INTERFAZ: Clic derecho al nombre de la clase "CategoryRepository", add, "quick action and refactoring, extract interface", OK.
//   g) Mover la interfaz creada a la carpeta "Interfaces"
//   --despues del punto 7, adicionar el punto h): --
//   h)adicionar la referencia entre la interfaz y la clase category en Program.cs, al final de // Add services to the container.
//     builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
//4.Para referenciar el proyecto de BIBLIOTECA al proyecto de API:
//   Clic derecho, add, project reference, seleccionar UE.STOREDB.DOMAIN, OK
//5. Agregar la cadena de conexión en appsettings.json: 
//   , 
//   "ConnectionStrings":{ "DevConnection": "Server=BRIGITETARA9440;Database=StoreDB;Integrated Security=true;TrustServerCertificate=True"}
//6.Agregar la conexión a Program.cs, en //Add services to the container: 
//   var _config = builer.Contiguration;
//var cnx = _config.GetConnectionString("DevConnection");
//builder.Services
//   .AddDbContext<PeruDbContext>(options => options.UseSqlServer(cnx));
//7.Crear el controlador: se crea un controlador por entidad, se usara categoria para el ejemplo.
//   - En la carpeta Controllers clic derecho, add, Controller ..., API, "API Controller Empty"; Name: CategoryController.cs
//   - Realizar el CRUD por ENTIDAD
//8. EJECUTAR: HTTP
//********************************** ULTIMA CLASE*********************************************************************
//**********************************USANDO DTO - SERVICES - INTERFACES - CONTROLLER**********************************
//9. En el proyecto de bibliotecas:
//   i) Crear DTOs: ir al a carpeta Infraestructure/DTO, clic derecho add, class, ProductDTO.cs, ingresar código
//   j) Crear Servicios: ir al a carpeta Core/Services, clic derecho add, class, ProductService.cs, ingresar código
//      considerar lo siguiente: se debe adicionar un DTO adicional de detailproduct si se desea mostrar este detalle en el getbyidforproductdetail
//	                           adicionar en el repositorio Product el include en el metod getbyid. Include(p => p.ProductDetail)   
//							   en el productdto se debe adicionar la siguiente linea:         public ICollection<ProductDetailListDTO> ProductDetail { get; set; }
//k) Crear la INTERFAZ: Clic derecho al nombre de la clase "ProductService", add, "quick action and refactoring, extract interface", OK.
//   l) Mover la interfaz creada a la carpeta "Interfaces"
//      --despues , adicionar el punto m): --
//   m)adicionar la referencia entre la interfaz y la clase servicio en Program.cs, al final de // Add services to the container.
//     builder.Services.AddTransient<IProductService, ProductService>();
//10.Crear el controlador: se crea un controlador por servicio, se usara product para el ejemplo.
//   - En la carpeta Controllers clic derecho, add, Controller ..., API, "API Controller Empty"; Name: ProductController.cs
//   - Realizar el CRUD por SERVICIO
//11. EJECUTAR: HTTP
//********************************** FIN*********************************************************************








