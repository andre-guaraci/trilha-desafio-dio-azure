
using Azure.Storage.Files.Shares.Models;
using CadastroFuncionarios.Models.Services;
using Funcionarios.Context;
using Funcionarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Linq;

namespace Funcionarios.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly RHContext _context;
        private readonly LogService _logService;

        public FuncionariosController(RHContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public IActionResult Index()
        {
            var funcionarios = _context.Funcionarios.ToList();
            return View(funcionarios);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();
                await _logService.SaveLogAsync(funcionario, TipoAcao.Inclusao);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);

            if (funcionario == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(Funcionario model)
        {
            var funcionarioExistente = _context.Funcionarios.FirstOrDefault(x => x.Id == model.Id);
            {
                if (funcionarioExistente == null)
                {
                    return NotFound();
                }

                funcionarioExistente.Nome = model.Nome;
                funcionarioExistente.Endereco = model.Endereco;
                funcionarioExistente.Ramal = model.Ramal;
                funcionarioExistente.EmailProfissional = model.EmailProfissional;
                funcionarioExistente.Departamento = model.Departamento;
                funcionarioExistente.Salario = model.Salario;
                funcionarioExistente.Ativo = model.Ativo;

                await _context.SaveChangesAsync();
                await _logService.SaveLogAsync(funcionarioExistente, TipoAcao.Atualizacao);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                return View(funcionario);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Deletar(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);

            if (funcionario != null)
            {
                return View(funcionario);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
                await _logService.SaveLogAsync(funcionario, TipoAcao.Remocao);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }
    }

}
