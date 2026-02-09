using FluentValidation;
using Inventario.Domain;

namespace Inventario.Application.Validators
{
    // Heredamos de AbstractValidator y le decimos qué clase vamos a validar
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            // Regla 1: El nombre es obligatorio y no puede ser muy largo
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres.");

            // Regla 2: La descripción también es obligatoria
            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.");

            // Regla 3: El precio debe ser mayor a 0
            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            // Regla 4: El stock no puede ser negativo (puede ser 0, pero no -1)
            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }
    }
}