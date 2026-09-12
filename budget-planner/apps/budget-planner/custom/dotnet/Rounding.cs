namespace BudgetPlanner.Custom;

/// <summary>
/// Rounding, which the expression language deliberately does not have.
///
/// <para>Cord has pow, min and max and stops there: every function it offers has one obvious answer
/// on every input, and rounding does not — half-up, half-even and toward-zero all disagree about
/// 2.5, and a plan that quietly picked one would be wrong for somebody. So the decision belongs to
/// the application, which is what this file is.</para>
///
/// <para>This plan rounds half AWAY from zero, because the figure is a count of companies and the
/// reader is a person: 2.5 customers reads as 3. Banker's rounding is the better default for money
/// and is deliberately not what happens here.</para>
/// </summary>
[CordangoFunctions]
public static class Rounding
{
    /// <summary>
    /// The nearest whole number, halves away from zero.
    ///
    /// <para>Null in, null out. A computed value can be unknown — a rollup over no rows, a figure
    /// that could not be worked out — and rounding an unknown has to stay unknown rather than
    /// becoming a confident zero.</para>
    /// </summary>
    [CordangoFunction("round", Description = "The nearest whole number, halves away from zero.")]
    public static decimal? Round(decimal? value) =>
        value is null ? null : decimal.Round(value.Value, 0, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Rounded to whole cents, halves to even.
    ///
    /// <para>Money, unlike a headcount: half-to-even is what accounting expects, because rounding
    /// half up on a long column of figures drifts upward by about half a cent per row.</para>
    /// </summary>
    [CordangoFunction("round_money", Description = "Whole cents, halves to even.")]
    public static decimal? RoundMoney(decimal? value) =>
        value is null ? null : decimal.Round(value.Value, 2, MidpointRounding.ToEven);
}
