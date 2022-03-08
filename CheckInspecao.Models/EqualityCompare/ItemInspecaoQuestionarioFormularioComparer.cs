using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CheckInspecao.Models.EqualityCompare
{
    public class
    ItemInspecaoQuestionarioFormularioComparer
    : IEqualityComparer<ItemInspecaoQuestionarioFormulario>
    {
        public bool
        Equals(
            ItemInspecaoQuestionarioFormulario x,
            ItemInspecaoQuestionarioFormulario y
        )
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null) return false;

            return (
            x.ItemInspecaoId == y.ItemInspecaoId &&
            x.QuestionarioFormularioId == y.QuestionarioFormularioId
            );
        }

        public int
        GetHashCode([DisallowNull] ItemInspecaoQuestionarioFormulario obj)
        {
            return (obj.ItemInspecaoId * obj.QuestionarioFormularioId)
                .GetHashCode();
        }
    }
}
