using Cordango.Standalone.Http;

namespace BudgetPlanner.Custom;

/// <summary>
/// The one rule about a plan's horizon that the definition cannot state.
///
/// <para>A scenario is modelled month by month for its first stretch and yearly after that, so
/// <c>monthly_months</c> has to fit inside <c>plan_years</c>. Every part of that is expressible —
/// both fields are integers, both are required — except the relationship BETWEEN them, and a
/// definition describes fields rather than the arithmetic that has to hold across them.</para>
///
/// <para><b>Why a hook and not a computed field.</b> A computed field would work the number out and
/// store it. This does not want a number: it wants the write to stop, with a sentence the person who
/// typed 84 months into a three-year plan can act on. Refusing is the thing only code can do.</para>
/// </summary>
[CordangoHooks]
public sealed class PlanWindow
{
    /// <summary>A new scenario, checked before anything is worked out from it.</summary>
    [BeforeCreate]
    public Task OnCreate(Scenario record, RecordContext context, CancellationToken ct)
    {
        Check(record);
        return Task.CompletedTask;
    }

    /// <summary>
    /// An edited scenario.
    ///
    /// <para>An update hook is handed both versions: <paramref name="record"/> carries the incoming
    /// change and <paramref name="before"/> is the row as it was. Only the first is checked here —
    /// the previous row was already valid — but the pair is what makes "did this field actually
    /// change" answerable, which is why the runtime passes it.</para>
    /// </summary>
    [BeforeUpdate]
    public Task OnUpdate(Scenario record, Scenario before, RecordContext context, CancellationToken ct)
    {
        Check(record);
        return Task.CompletedTask;
    }

    private static void Check(Scenario scenario)
    {
        var months = scenario.MonthlyMonths;
        var years = scenario.PlanYears;

        if (months is null || years is null) return;

        var available = years.Value * 12;
        if (months.Value <= available) return;

        throw new RecordException(
            "scenario.monthly_window",
            $"The plan runs {years} years, which is {available} months, and this asks for "
            + $"{months} months of monthly detail. Shorten the monthly window, or lengthen the plan.",
            fields: ["monthly_months"]);
    }
}
