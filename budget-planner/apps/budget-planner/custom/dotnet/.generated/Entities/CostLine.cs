using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Cost Line. A non-payroll cost: fixed monthly, scaling per user, or a one-off payment such as
/// a German government fee.
/// </summary>
public sealed class CostLine : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Scenario. Holds the id of a scenario record.
    /// </summary>
    [JsonPropertyName("scenario")] public string Scenario { get; set; } = "";

    /// <summary>
    /// Category. Holds the id of a cost_category record.
    /// </summary>
    [JsonPropertyName("cost_category")] public string? CostCategory { get; set; }

    /// <summary>
    /// Behaviour. One of: fixed_monthly, per_user, one_off, percent_of_revenue.
    /// </summary>
    [JsonPropertyName("cost_behaviour")] public string CostBehaviour { get; set; } = "";

    /// <summary>
    /// Fixed Amount / Month. Used when behaviour is Fixed monthly. In EUR.
    /// </summary>
    [JsonPropertyName("monthly_amount")] public decimal? MonthlyAmount { get; set; }

    /// <summary>
    /// Amount per User / Month. Used when behaviour is Per user. In EUR.
    /// </summary>
    [JsonPropertyName("amount_per_user")] public decimal? AmountPerUser { get; set; }

    /// <summary>
    /// One-off Amount. Used when behaviour is One-off. In EUR.
    /// </summary>
    [JsonPropertyName("one_off_amount")] public decimal? OneOffAmount { get; set; }

    [JsonPropertyName("one_off_date")] public DateOnly? OneOffDate { get; set; }

    [JsonPropertyName("start_month")] public DateOnly? StartMonth { get; set; }

    [JsonPropertyName("end_month")] public DateOnly? EndMonth { get; set; }

    /// <summary>
    /// Annual Increase. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("annual_increase_rate")] public decimal? AnnualIncreaseRate { get; set; }

    /// <summary>
    /// Government / Statutory Fee. Trade register, IHK, notary, Finanzamt, GmbH filing and similar
    /// German statutory costs.
    /// </summary>
    [JsonPropertyName("is_government_fee")] public bool? IsGovernmentFee { get; set; }

    [JsonPropertyName("vendor")] public string? Vendor { get; set; }

    [JsonPropertyName("notes")] public string? Notes { get; set; }

    /// <summary>
    /// Percent of Revenue. Used when behaviour is % of revenue — payment and collection fees,
    /// revenue share, affiliate commission. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("percent_amount")] public decimal? PercentAmount { get; set; }

    /// <summary>
    /// Percentage Of. Which figure the percentage applies to. Both are always positive, so the fee
    /// can never come out as a credit. One of: revenue, payroll.
    /// </summary>
    [JsonPropertyName("percent_basis")] public string? PercentBasis { get; set; }

    /// <summary>
    /// One-off Month. Month of the plan this is paid, counting from month 1. Use this OR the exact
    /// date, whichever you actually know.
    /// </summary>
    [JsonPropertyName("one_off_month")] public long? OneOffMonth { get; set; }

    /// <summary>
    /// Created. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("created_at")] public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Created By. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("created_by")] public string? CreatedBy { get; set; }

    /// <summary>
    /// Updated. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("updated_at")] public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Updated By. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("updated_by")] public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Deleted. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("deleted_at")] public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    /// Record state. One of: active, archived. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("record_state")] public string? RecordState { get; set; }
}
