using Xunit;
using Microsoft.EntityFrameworkCore;
using MicroservicioClientePersona.Controllers;
using MicroservicioClientePersona.Data;
using MicroservicioClientePersona.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MicroserviciosPruebas
{
    public class ClienteControllerUnitTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ClienteController _controller;

        public ClienteControllerUnitTests()
        {
            // Configuramos una base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Creamos el contexto utilizando la base de datos en memoria
            _context = new ApplicationDbContext(options);

            // Inicializamos el controlador con el contexto
            _controller = new ClienteController(_context);

            // Llenamos la base de datos en memoria con datos de prueba
            SeedDatabase();
        }

        // Función para llenar la base de datos en memoria con datos de prueba
        private void SeedDatabase()
        {
            // Primero creamos una Persona
            var persona = new Persona
            {
                PersonaId = 1,
                Nombre = "John Doe",
                Edad = 30,
                Genero = "Masculino",
                Identificacion = "1234567890",
                Direccion = "123 Calle Falsa",
                Telefono = "9876543210"
            };

            // Luego asociamos esta Persona al Cliente
            var cliente = new Cliente
            {
                ClienteId = 1,
                PersonaId = persona.PersonaId,  // Relación con la Persona
                Contraseña = "password123",
                Estado = true,
                Persona = persona // Asociación explícita
            };

            _context.Personas.Add(persona);
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetClienteById_ReturnsCliente()
        {
            // Act: llamamos al método del controlador
            var result = await _controller.GetCliente(1);

            // Assert: verificamos que el resultado sea exitoso y que sea el cliente esperado
            var okResult = Assert.IsType<ActionResult<Cliente>>(result);
            var clienteResultado = Assert.IsType<Cliente>(okResult.Value);

            // Verificar el ID del cliente
            Assert.Equal(1, clienteResultado.ClienteId);

            // Verificar que los datos de la Persona estén correctamente asociados
            Assert.Equal("John Doe", clienteResultado.Persona.Nombre);
            Assert.Equal("1234567890", clienteResultado.Persona.Identificacion);
        }
    }
}