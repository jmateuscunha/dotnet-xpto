using System.ComponentModel.DataAnnotations;

namespace Xpto.Application.Dtos;

public record CreateWalletDto([Required]string Name, [Required]string ChainId);