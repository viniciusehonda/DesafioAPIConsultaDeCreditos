using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Application.Abstractions.Messaging;

public interface ICommand;

public interface ICommand<TResponse>;
