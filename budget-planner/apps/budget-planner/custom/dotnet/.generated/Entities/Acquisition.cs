using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Acquisition. New customers won in one month, in one segment. This is the one table in the
/// revenue model somebody types into.
/// </summary>
public sealed class Acquisition : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("label")] public string Label { get; set; } = "";

    /// <summary>
    /// Scenario. Holds the id of a scenario record.
    /// </summary>
    [JsonPropertyName("scenario")] public string Scenario { get; set; } = "";

    /// <summary>
    /// Segment. Holds the id of a segment record.
    /// </summary>
    [JsonPropertyName("segment")] public string Segment { get; set; } = "";

    /// <summary>
    /// Plan month. Counted from the plan start. Month 1 is the first month.
    /// </summary>
    [JsonPropertyName("month")] public long Month { get; set; }

    /// <summary>
    /// New customers. Typed. Everything downstream of it is derived.
    /// </summary>
    [JsonPropertyName("new_customers")] public long? NewCustomers { get; set; }

    /// <summary>
    /// Setup & services. Invoiced once, in the month the customers are won. In EUR. Set by the
    /// runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("services_revenue")] public decimal? ServicesRevenue { get; set; }

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
