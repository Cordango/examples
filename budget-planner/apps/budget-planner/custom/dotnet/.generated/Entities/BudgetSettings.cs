using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Planner Settings. App-wide defaults for new scenarios and German employer cost rules.
/// </summary>
public sealed class BudgetSettings : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("company_name")] public string? CompanyName { get; set; }

    /// <summary>
    /// Legal Form. One of: ug, gmbh, ag, gbr, other.
    /// </summary>
    [JsonPropertyName("legal_form")] public string? LegalForm { get; set; }

    /// <summary>
    /// Base Currency. One of: EUR, USD, CHF.
    /// </summary>
    [JsonPropertyName("base_currency")] public string? BaseCurrency { get; set; }

    [JsonPropertyName("default_plan_years")] public long? DefaultPlanYears { get; set; }

    [JsonPropertyName("default_monthly_months")] public long? DefaultMonthlyMonths { get; set; }

    /// <summary>
    /// Default Employer Cost Rate. Employer social contributions on top of gross salary (Kranken-,
    /// Renten-, Arbeitslosen-, Pflegeversicherung, U1/U2). Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("default_employer_cost_rate")] public decimal? DefaultEmployerCostRate { get; set; }

    [JsonPropertyName("apply_employer_costs")] public bool? ApplyEmployerCosts { get; set; }

    /// <summary>
    /// VAT Rate. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("vat_rate")] public decimal? VatRate { get; set; }

    /// <summary>
    /// Corporate Tax Rate. Körperschaftsteuer + Solidaritätszuschlag + Gewerbesteuer combined.
    /// Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("corporate_tax_rate")] public decimal? CorporateTaxRate { get; set; }

    [JsonPropertyName("runway_alert_months")] public decimal? RunwayAlertMonths { get; set; }

    [JsonPropertyName("investor_notes")] public string? InvestorNotes { get; set; }

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
