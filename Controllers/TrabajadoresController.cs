using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TrabajadoresApp.Data;
using TrabajadoresApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace TrabajadoresApp.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresDbContext _context;

        public TrabajadoresController(TrabajadoresDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filtroSexo)
        {
            var paramSexo = new SqlParameter("@Sexo", string.IsNullOrEmpty(filtroSexo) ? DBNull.Value : filtroSexo);
            var lista = await _context.Trabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores @Sexo", paramSexo)
                .ToListAsync();

            ViewBag.FiltroSexo = filtroSexo ?? "";
            return View(lista);
        }

        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_InsertarTrabajador @TipoDocumento, @NumeroDocumento, @Nombres, @Sexo, @IdDepartamento, @IdProvincia, @IdDistrito",
                        new SqlParameter("@TipoDocumento", trabajador.TipoDocumento ?? (object)DBNull.Value),
                        new SqlParameter("@NumeroDocumento", trabajador.NumeroDocumento ?? (object)DBNull.Value),
                        new SqlParameter("@Nombres", trabajador.Nombres ?? (object)DBNull.Value),
                        new SqlParameter("@Sexo", trabajador.Sexo ?? (object)DBNull.Value),
                        new SqlParameter("@IdDepartamento", trabajador.IdDepartamento ?? (object)DBNull.Value),
                        new SqlParameter("@IdProvincia", trabajador.IdProvincia ?? (object)DBNull.Value),
                        new SqlParameter("@IdDistrito", trabajador.IdDistrito ?? (object)DBNull.Value)
                    );
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Error al guardar el trabajador.");
                }
            }

            CargarCombos(trabajador);
            return View(trabajador);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lista = await _context.Trabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores @Sexo", new SqlParameter("@Sexo", DBNull.Value))
                .ToListAsync();

            var trabajador = lista.FirstOrDefault(t => t.Id == id);
            if (trabajador == null) return NotFound();

            CargarCombos(trabajador);
            return View(trabajador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (id != trabajador.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_ActualizarTrabajador @Id, @TipoDocumento, @NumeroDocumento, @Nombres, @Sexo, @IdDepartamento, @IdProvincia, @IdDistrito",
                        new SqlParameter("@Id", trabajador.Id),
                        new SqlParameter("@TipoDocumento", trabajador.TipoDocumento ?? (object)DBNull.Value),
                        new SqlParameter("@NumeroDocumento", trabajador.NumeroDocumento ?? (object)DBNull.Value),
                        new SqlParameter("@Nombres", trabajador.Nombres ?? (object)DBNull.Value),
                        new SqlParameter("@Sexo", trabajador.Sexo ?? (object)DBNull.Value),
                        new SqlParameter("@IdDepartamento", trabajador.IdDepartamento ?? (object)DBNull.Value),
                        new SqlParameter("@IdProvincia", trabajador.IdProvincia ?? (object)DBNull.Value),
                        new SqlParameter("@IdDistrito", trabajador.IdDistrito ?? (object)DBNull.Value)
                    );
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Error al actualizar el trabajador.");
                }
            }

            CargarCombos(trabajador);
            return View(trabajador);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lista = await _context.Trabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores @Sexo", new SqlParameter("@Sexo", DBNull.Value))
                .ToListAsync();

            var trabajador = lista.FirstOrDefault(t => t.Id == id);
            if (trabajador == null) return NotFound();

            return View(trabajador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarTrabajador @Id", new SqlParameter("@Id", id));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el trabajador.");
            }

            return RedirectToAction(nameof(Index)); 
        }

        private void CargarCombos(Trabajador trabajador = null)
        {
            ViewBag.TiposDocumento = new SelectList(new[]
            {
                new { Value = "DNI", Text = "DNI" },
                new { Value = "CE", Text = "Carné de Extranjería" },
                new { Value = "PAS", Text = "Pasaporte" }
            }, "Value", "Text", trabajador?.TipoDocumento);

            ViewBag.Departamentos = new SelectList(_context.Departamentos.ToList(), "Id", "NombreDepartamento", trabajador?.IdDepartamento);
            ViewBag.Provincias = new SelectList(_context.Provincias.ToList(), "Id", "NombreProvincia", trabajador?.IdProvincia);
            ViewBag.Distritos = new SelectList(_context.Distritos.ToList(), "Id", "NombreDistrito", trabajador?.IdDistrito);
        }
    }
}
