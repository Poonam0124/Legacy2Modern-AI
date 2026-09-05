using System.Runtime.InteropServices;

namespace Legacy2Modern.Business.Models
{
    public enum ModernizationStrategy
    {
        Refactor,
        Extract,
        Replace,
        Reconfigure,
        Decouple,
        Migrate,
        Rewrite
    }
}
//| Strategy      | Meaning                                                  |
//| ------------- | -------------------------------------------------------- |
//| `Refactor`    | Improve existing code without changing its core behavior |
//| `Extract`     | Move functionality into a separate component/service     |
//| `Replace`     | Replace an existing implementation with a better one     |
//| `Reconfigure` | Move hard-coded settings/rules into configuration        |
//| `Decouple`    | Remove tight dependencies between layers/components      |
//| `Migrate`     | Move functionality/data to a modern platform             |
//| `Rewrite`     | Rebuild functionality using a modern architecture |
