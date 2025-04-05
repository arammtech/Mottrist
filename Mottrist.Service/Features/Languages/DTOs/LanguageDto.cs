using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Languages.DTOs
{
    /// <summary>
    /// Data Transfer Object for languages.
    /// </summary>
    public class LanguageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the language.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
