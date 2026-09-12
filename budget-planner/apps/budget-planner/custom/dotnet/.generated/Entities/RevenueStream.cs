using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Revenue Stream. Revenue that is not the subscription: onboarding and migration, custom app
/// development, consulting, white-label licensing, marketplace. Kept separate because none of
/// it is recurring platform revenue.
/// </summary>
public sealed class RevenueStream : IRecord, IHasTrackingFields
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
    /// Kind. One of: onboarding, custom_development, consulting, white_label, marketplace, other.
    /// </summary>
    [JsonPropertyName("stream_type")] public string StreamType { get; set; } = "";

    /// <summary>
    /// Behaviour. One of: recurring_monthly, one_off.
    /// </summary>
    [JsonPropertyName("revenue_behaviour")] public string RevenueBehaviour { get; set; } = "";

    /// <summary>
    /// Amount / Month. Used when behaviour is Recurring monthly. In EUR.
    /// </summary>
    [JsonPropertyName("monthly_amount")] public decimal? MonthlyAmount { get; set; }

    /// <summary>
    /// One-off Amount. In EUR.
    /// </summary>
    [JsonPropertyName("one_off_amount")] public decimal? OneOffAmount { get; set; }

    /// <summary>
    /// One-off Month. Month of the plan this is invoiced, counting from month 1. Use this OR the
    /// exact date, whichever you actually know.
    /// </summary>
    [JsonPropertyName("one_off_month")] public long? OneOffMonth { get; set; }

    [JsonPropertyName("one_off_date")] public DateOnly? OneOffDate { get; set; }

    /// <summary>
    /// Paid out to the creator. Marketplace split. The working assumption is 20% to the app creator
    /// and 80% to Cordango, but it is not settled, so it is a number rather than a rule. Displayed
    /// with the unit '%'.
    /// </summary>
    [JsonPropertyName("creator_share_pct")] public decimal? CreatorSharePct { get; set; }

    [JsonPropertyName("start_month")] public DateOnly? StartMonth { get; set; }

    /// <summary>
    /// Ends. Leave empty for open-ended.
    /// </summary>
    [JsonPropertyName("end_month")] public DateOnly? EndMonth { get; set; }

    /// <summary>
    /// Net / Month. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("net_monthly_amount")] public decimal? NetMonthlyAmount { get; set; }

    /// <summary>
    /// Net one-off. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("net_one_off_amount")] public decimal? NetOneOffAmount { get; set; }

    [JsonPropertyName("customer")] public string? Customer { get; set; }

    [JsonPropertyName("notes")] public string? Notes { get; set; }

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
