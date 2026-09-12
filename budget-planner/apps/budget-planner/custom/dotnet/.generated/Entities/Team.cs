using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Team. A functional team hiring is grouped by (Engineering, Sales, ...).
/// </summary>
public sealed class Team : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Function. One of: engineering, product, sales, marketing, customer_success, operations,
    /// finance.
    /// </summary>
    [JsonPropertyName("function")] public string Function { get; set; } = "";

    /// <summary>
    /// Default Gross Salary / Year. Typical gross annual salary for a hire in this team; prefilled
    /// onto new hiring lines. In EUR.
    /// </summary>
    [JsonPropertyName("default_salary")] public decimal? DefaultSalary { get; set; }

    [JsonPropertyName("description")] public string? Description { get; set; }

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
