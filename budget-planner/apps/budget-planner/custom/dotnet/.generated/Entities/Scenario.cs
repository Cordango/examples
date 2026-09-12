using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Scenario. One named version of the budget plan (e.g. Conservative, Base, Optimistic) that
/// everything else hangs off.
/// </summary>
public sealed class Scenario : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Stage. One of: draft, modelling, review, investor_ready, archived.
    /// </summary>
    [JsonPropertyName("scenario_stage")] public string? ScenarioStage { get; set; }

    /// <summary>
    /// Case. One of: conservative, base, optimistic.
    /// </summary>
    [JsonPropertyName("case_type")] public string CaseType { get; set; } = "";

    [JsonPropertyName("summary")] public string? Summary { get; set; }

    [JsonPropertyName("assumptions")] public string? Assumptions { get; set; }

    [JsonPropertyName("plan_start")] public DateOnly PlanStart { get; set; }

    /// <summary>
    /// Plan Length (years). Displayed with the unit ' yr'.
    /// </summary>
    [JsonPropertyName("plan_years")] public long? PlanYears { get; set; }

    /// <summary>
    /// Monthly Detail (months). Number of months modelled monthly before the plan switches to
    /// yearly periods. Displayed with the unit ' mo'.
    /// </summary>
    [JsonPropertyName("monthly_months")] public long? MonthlyMonths { get; set; }

    /// <summary>
    /// Currency. One of: EUR, USD, CHF, GBP.
    /// </summary>
    [JsonPropertyName("currency_code")] public string? CurrencyCode { get; set; }

    /// <summary>
    /// Starting Cash. In EUR.
    /// </summary>
    [JsonPropertyName("starting_cash")] public decimal? StartingCash { get; set; }

    /// <summary>
    /// Total Funding Raised. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_funding")] public decimal? TotalFunding { get; set; }

    /// <summary>
    /// Total Plan Revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_revenue")] public decimal? TotalRevenue { get; set; }

    /// <summary>
    /// Total Plan Costs. Cost of revenue, people and operating costs across every period. In EUR.
    /// Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_costs")] public decimal? TotalCosts { get; set; }

    /// <summary>
    /// Net Result. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("net_result")] public decimal? NetResult { get; set; }

    /// <summary>
    /// Cash At Plan End. Opening cash plus everything the periods moved — the same number the last
    /// period's Cash at end shows. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cash_at_end")] public decimal? CashAtEnd { get; set; }

    /// <summary>
    /// Runway (months). Months of cover at the plan's average monthly spend. Displayed with the
    /// unit ' mo'. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("runway_months")] public decimal? RunwayMonths { get; set; }

    /// <summary>
    /// Periods Modelled. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("period_count")] public long? PeriodCount { get; set; }

    /// <summary>
    /// Planned Headcount (end). Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("headcount_end")] public long? HeadcountEnd { get; set; }

    [JsonPropertyName("shared_with_investors")] public bool? SharedWithInvestors { get; set; }

    [JsonPropertyName("last_reviewed_at")] public DateTimeOffset? LastReviewedAt { get; set; }

    /// <summary>
    /// Owner. Holds the id of a person record. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("owner")] public string? Owner { get; set; }

    /// <summary>
    /// Plan months. How many monthly periods to generate for this plan
    /// </summary>
    [JsonPropertyName("plan_months")] public long? PlanMonths { get; set; }

    /// <summary>
    /// Total Tax Provision. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_tax")] public decimal? TotalTax { get; set; }

    /// <summary>
    /// Net Cash Movement. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_cash_movement")] public decimal? TotalCashMovement { get; set; }

    /// <summary>
    /// Fixed costs / month. Summary of the Costs & Fees tab. Change it there. Set by the runtime;
    /// not editable in a form.
    /// </summary>
    [JsonPropertyName("monthly_fixed_costs")] public decimal? MonthlyFixedCosts { get; set; }

    /// <summary>
    /// Revenue-share fees. Summary of the Costs & Fees tab. Change it there. Set by the runtime;
    /// not editable in a form.
    /// </summary>
    [JsonPropertyName("revenue_fee_rate")] public decimal? RevenueFeeRate { get; set; }

    /// <summary>
    /// One-off costs. Summary of the Costs & Fees tab. Change it there. Set by the runtime; not
    /// editable in a form.
    /// </summary>
    [JsonPropertyName("one_off_costs")] public decimal? OneOffCosts { get; set; }

    /// <summary>
    /// Services & Other Revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_other_revenue")] public decimal? TotalOtherRevenue { get; set; }

    /// <summary>
    /// AI billed to customers. Share of recurring MRR billed on for AI. The workbook carries 6% as
    /// a COST; billing it on at the same rate is pure pass-through. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("ai_charge_rate")] public decimal? AiChargeRate { get; set; }

    /// <summary>
    /// Provider discount. How much less we pay the provider than we bill. This is the AI margin: we
    /// ask the customer a published rate and may buy cheaper. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("ai_provider_discount")] public decimal? AiProviderDiscount { get; set; }

    /// <summary>
    /// Total Recurring Revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_recurring")] public decimal? TotalRecurring { get; set; }

    /// <summary>
    /// Total Services Revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_services")] public decimal? TotalServices { get; set; }

    /// <summary>
    /// Total AI Margin. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_ai_margin")] public decimal? TotalAiMargin { get; set; }

    /// <summary>
    /// Customers Won. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("customers_won")] public long? CustomersWon { get; set; }

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
