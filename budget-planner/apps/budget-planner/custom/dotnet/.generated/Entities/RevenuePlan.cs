using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Plan. One row of the published price list: a base fee, a rate per active user x app, and the
/// cap factor k. Nothing here says who buys it — a customer lands on whichever plan is cheapest
/// for their shape, so the plan is an outcome.
/// </summary>
public sealed class RevenuePlan : IRecord, IHasTrackingFields
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
    /// Tier. One of: flex, starter, pro, advanced, enterprise.
    /// </summary>
    [JsonPropertyName("tier")] public string Tier { get; set; } = "";

    /// <summary>
    /// Base fee / month. Charged whatever the usage is. Zero on Flex. In EUR.
    /// </summary>
    [JsonPropertyName("monthly_base_fee")] public decimal? MonthlyBaseFee { get; set; }

    /// <summary>
    /// Rate / active user x app. One unit is one person actively using one shared app this month.
    /// In EUR.
    /// </summary>
    [JsonPropertyName("price_per_app_user")] public decimal? PricePerAppUser { get; set; }

    /// <summary>
    /// Cap factor k. Billable users for ONE app stop at k x the square root of the customer's
    /// active users. Blank on Flex (no cap) and on Enterprise (negotiated).
    /// </summary>
    [JsonPropertyName("cap_multiplier")] public decimal? CapMultiplier { get; set; }

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
