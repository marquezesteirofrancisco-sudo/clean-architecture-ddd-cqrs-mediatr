using CleanArchitectureTemplate.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosQuery : IRequest<List<ProductDTO>>{}
}
