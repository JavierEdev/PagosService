# Pagos Service

Este servicio procesa pagos de reservas, registrando la transacción y actualizando el estado de la reserva a **pagada**. Expone una API REST y utiliza Entity Framework Core para la persistencia.

## 🚀 Tecnologías
- .NET 8
- C#
- MySQL
- Pomelo.EntityFrameworkCore.MySql
- Clean Architecture
- Adapters
- Abstract Factory

## 📂 Estructura
PagosService/
├── Controllers/
├── Domain/
├── Application/
├── Infrastructure/
└── Program.cs


## 🔌 Endpoints

| Método | Ruta          | Descripción                        |
|--------|---------------|------------------------------------|
| POST   | `/api/pagos` | Procesa un pago y actualiza reserva|

### Payload esperado:
```json
{
  "idReserva": 1,
  "monto": 200.0,
  "correoCliente": "cliente@ejemplo.com",
  "tarjeta": "4111111111111111"
}

"ConnectionStrings": {
  "MySql": "server=localhost;database=sistema_reservas;user=root;password=tu_clave"
}

dotnet build
dotnet run

Accede a la documentación interactiva en:
👉 http://localhost:{puerto}/swagger


