using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Cohort Month. One cohort at one age. The grid exists because a rollup can add a field up but
/// cannot multiply two rows together, and month m's revenue is exactly that product summed over
/// every live cohort.
/// </summary>
public sealed class CohortMonth : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("label")] public string? Label { get; set; }

    /// <summary>
    /// Scenario. Holds the id of a scenario record.
    /// </summary>
    [JsonPropertyName("scenario")] public string Scenario { get; set; } = "";

    /// <summary>
    /// Cohort. Holds the id of a acquisition record.
    /// </summary>
    [JsonPropertyName("acquisition")] public string Acquisition { get; set; } = "";

    /// <summary>
    /// Lifecycle step. Holds the id of a lifecycle_step record.
    /// </summary>
    [JsonPropertyName("step")] public string Step { get; set; } = "";

    /// <summary>
    /// Lands in month. The cohort's own month plus its age, less one. Two references, one hop each
    /// — which is the rule, and is what makes the grid legal. Set by the runtime; not editable in a
    /// form.
    /// </summary>
    [JsonPropertyName("lands_in")] public long? LandsIn { get; set; }

    /// <summary>
    /// Recurring revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("recurring_revenue")] public decimal? RecurringRevenue { get; set; }

    /// <summary>
    /// Surviving customers. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("surviving_customers")] public decimal? SurvivingCustomers { get; set; }

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
