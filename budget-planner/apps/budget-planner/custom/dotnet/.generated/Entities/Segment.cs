using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Segment. A band of company size, described at MATURITY. A customer arrives small and grows
/// into these numbers along the adoption curve.
/// </summary>
public sealed class Segment : IRecord, IHasTrackingFields
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
    /// Mature active users. Active Cordango users in the company once it is fully rolled out. This
    /// is N in the cap formula.
    /// </summary>
    [JsonPropertyName("mature_active_users")] public long MatureActiveUsers { get; set; }

    [JsonPropertyName("mature_shared_apps")] public decimal MatureSharedApps { get; set; }

    /// <summary>
    /// Average app adoption. Share of the company's active users who use any ONE given app.
    /// Multiplied by active users to get A, the per-app population the cap bounds.
    /// </summary>
    [JsonPropertyName("avg_app_adoption")] public decimal? AvgAppAdoption { get; set; }

    /// <summary>
    /// Setup / services fee. One-off, invoiced in the month the customer is won. In EUR.
    /// </summary>
    [JsonPropertyName("setup_fee")] public decimal? SetupFee { get; set; }

    /// <summary>
    /// Churn / month. Applied as pow(1 - rate, age - 1) along the cohort. The workbook defines this
    /// per segment and never applies it; here it does. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("churn_pct")] public decimal? ChurnPct { get; set; }

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
