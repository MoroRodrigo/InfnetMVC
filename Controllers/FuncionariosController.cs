using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InfnetMVC.Models;
using InfnetMVC.Models.InfnetMVC.Models;

namespace InfnetMVC.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly InfnetDbContext _context;

        public FuncionariosController(InfnetDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            // Lista de departamentos para o dropdown
            ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome");
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone,Email,DataNascimento,DepartamentoId")] Funcionario funcionario)
        {

                try
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    // Redireciona para a página Index após a criação bem-sucedida
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Se ocorrer um erro ao salvar no banco de dados, capture e trate o erro conforme necessário
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar o funcionário. Por favor, tente novamente.");
                    // Poderia adicionar o log do erro para debug também
                }
   
            // Se houver erro, recarrega a lista de departamentos
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            return View(funcionario);
        }


        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            // Lista de departamentos para o dropdown
            ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone,Email,DataNascimento,DepartamentoId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            try
            {
                // Verifica se o departamento associado ao funcionário existe
                var existingDepartamento = await _context.Departamentos.FindAsync(funcionario.DepartamentoId);
                if (existingDepartamento == null)
                {
                    ModelState.AddModelError("DepartamentoId", "Departamento inválido.");
                    // Recarrega a lista de departamentos
                    ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
                    return View(funcionario);
                }

                // Atualiza o funcionário
                _context.Update(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Se houver erro, recarrega a lista de departamentos
            ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", funcionario.DepartamentoId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
