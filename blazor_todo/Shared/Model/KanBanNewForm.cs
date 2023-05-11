using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanBanNewForm
	{
		[Required]
		[StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
		public string Name { get; set; }
	}
}
