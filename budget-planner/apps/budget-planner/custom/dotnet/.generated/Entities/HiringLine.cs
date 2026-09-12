using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Hiring Line. Planned headcount for a team from a given start month, with salary and employer
/// costs.
/// </summary>
public sealed class HiringLine : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("role_title")] public string RoleTitle { get; set; } = "";

    /// <summary>
    /// Scenario. Holds the id of a scenario record.
    /// </summary>
    [JsonPropertyName("scenario")] public string Scenario { get; set; } = "";

    /// <summary>
    /// Team. Holds the id of a team record.
    /// </summary>
    [JsonPropertyName("team")] public string Team { get; set; } = "";

    /// <summary>
    /// Hiring Status. One of: planned, approved, recruiting, filled, on_hold, dropped.
    /// </summary>
    [JsonPropertyName("hire_status")] public string? HireStatus { get; set; }

    [JsonPropertyName("headcount")] public long Headcount { get; set; }

    /// <summary>
    /// Seniority. One of: junior, mid, senior, lead, exec.
    /// </summary>
    [JsonPropertyName("seniority")] public string? Seniority { get; set; }

    /// <summary>
    /// Employment Type. One of: full_time, part_time, contractor, working_student.
    /// </summary>
    [JsonPropertyName("employment_type")] public string? EmploymentType { get; set; }

    [JsonPropertyName("start_month")] public DateOnly StartMonth { get; set; }

    /// <summary>
    /// Planned End. Leave empty for an open-ended role.
    /// </summary>
    [JsonPropertyName("end_month")] public DateOnly? EndMonth { get; set; }

    /// <summary>
    /// Location. One of: DE, AT, CH, NL, PL, PT, ES, US.
    /// </summary>
    [JsonPropertyName("location_country")] public string? LocationCountry { get; set; }

    /// <summary>
    /// Gross Salary / Year (per head). In EUR.
    /// </summary>
    [JsonPropertyName("gross_salary")] public decimal GrossSalary { get; set; }

    /// <summary>
    /// Employer Cost Rate. German employer social contributions on top of gross (approx. 20-22%).
    /// Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("employer_cost_rate")] public decimal? EmployerCostRate { get; set; }

    /// <summary>
    /// Other Cost per Head / Year. Equipment, tooling, recruiting fee amortised per head per year.
    /// In EUR.
    /// </summary>
    [JsonPropertyName("other_cost_per_head")] public decimal? OtherCostPerHead { get; set; }

    /// <summary>
    /// Loaded Cost per Head / Year. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("loaded_cost_per_head")] public decimal? LoadedCostPerHead { get; set; }

    /// <summary>
    /// Annual Cost (all heads). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("annual_cost")] public decimal? AnnualCost { get; set; }

    /// <summary>
    /// Monthly Cost (all heads). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("monthly_cost")] public decimal? MonthlyCost { get; set; }

    [JsonPropertyName("notes")] public string? Notes { get; set; }

    /// <summary>
    /// One-off Cost per Head. Equipment, recruiter fee and onboarding — charged once, in the month
    /// the role starts. In EUR.
    /// </summary>
    [JsonPropertyName("setup_cost_per_head")] public decimal? SetupCostPerHead { get; set; }

    /// <summary>
    /// One-off Cost (all heads). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("setup_cost_total")] public decimal? SetupCostTotal { get; set; }

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
