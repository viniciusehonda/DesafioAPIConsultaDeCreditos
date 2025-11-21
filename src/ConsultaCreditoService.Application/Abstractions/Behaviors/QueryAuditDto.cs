using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace ConsultaCreditoService.Application.Abstractions.Behaviors;
public record QueryAuditDto(string QueryName, bool IsSuccess, Error Error);
