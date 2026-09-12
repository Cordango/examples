using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Period. One modelled period of a scenario (month in year 1, then year): users, revenue,
/// costs and cash.
/// </summary>
public sealed class Period : IRecord, IHasTrackingFields
{
    /// <summary>The record's identity. A string, because a Cordango id may be a
    /// generated key or a handle somebody typed.</summary>
    [JsonPropertyName("id")] public string Id { get; set; } = "";

    /// <summary>
    /// Period. e.g. 2025-01 or FY2027.
    /// </summary>
    [JsonPropertyName("label")] public string Label { get; set; } = "";

    /// <summary>
    /// Scenario. Holds the id of a scenario record.
    /// </summary>
    [JsonPropertyName("scenario")] public string Scenario { get; set; } = "";

    /// <summary>
    /// Granularity. One of: month, year.
    /// </summary>
    [JsonPropertyName("period_type")] public string PeriodType { get; set; } = "";

    [JsonPropertyName("start_date")] public DateOnly StartDate { get; set; }

    [JsonPropertyName("end_date")] public DateOnly? EndDate { get; set; }

    [JsonPropertyName("sequence")] public long Sequence { get; set; }

    /// <summary>
    /// Tax provision. Tax provision — a judgement, not a derivation
    /// </summary>
    [JsonPropertyName("tax_provision")] public decimal? TaxProvision { get; set; }

    /// <summary>
    /// Actual revenue. What actually came in, once the month has closed
    /// </summary>
    [JsonPropertyName("actual_revenue")] public decimal? ActualRevenue { get; set; }

    /// <summary>
    /// Actual costs. What actually went out, once the month has closed
    /// </summary>
    [JsonPropertyName("actual_costs")] public decimal? ActualCosts { get; set; }

    /// <summary>
    /// Actual customers. Actual active users at month end
    /// </summary>
    [JsonPropertyName("actual_customers")] public long? ActualCustomers { get; set; }

    /// <summary>
    /// People cost (loaded). Sum of the hiring lines covering this month, each already carrying its
    /// own employer cost rate. Nobody types this, and nothing adds a second burden on top. Set by
    /// the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("payroll_cost")] public decimal? PayrollCost { get; set; }

    /// <summary>
    /// Total operating expenses. Sum of the cost lines whose months cover this period — marketing,
    /// tooling, insurance, contingency, statutory fees. Nobody types this. Set by the runtime; not
    /// editable in a form.
    /// </summary>
    [JsonPropertyName("opex_total")] public decimal? OpexTotal { get; set; }

    /// <summary>
    /// Headcount. Heads on the payroll this period. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("headcount")] public long? Headcount { get; set; }

    /// <summary>
    /// Active customers (exact). Carried to four places so churn stays visible on a small base. The
    /// whole-customer figure beside it is what people read. Set by the runtime; not editable in a
    /// form.
    /// </summary>
    [JsonPropertyName("active_exact")] public decimal? ActiveExact { get; set; }

    /// <summary>
    /// Active customers. Whole companies. The fraction is carried in the exact figure beside this
    /// one and rounded once, here. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("active_customers")] public long? ActiveCustomers { get; set; }

    /// <summary>
    /// Gross profit. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("gross_profit")] public decimal? GrossProfit { get; set; }

    /// <summary>
    /// Operating result. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("operating_result")] public decimal? OperatingResult { get; set; }

    /// <summary>
    /// Result before tax. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("result_before_tax")] public decimal? ResultBeforeTax { get; set; }

    /// <summary>
    /// One-off costs (dated). One-off payments dated inside this period — formation, equipment,
    /// statutory fees. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("one_off_dated")] public decimal? OneOffDated { get; set; }

    /// <summary>
    /// Funding received. Rounds closing inside this period. Money in, so it lifts the cash line.
    /// Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("funding_in")] public decimal? FundingIn { get; set; }

    /// <summary>
    /// Operating & one-off costs. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("other_costs")] public decimal? OtherCosts { get; set; }

    /// <summary>
    /// Total monthly cost. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("total_cost")] public decimal? TotalCost { get; set; }

    /// <summary>
    /// Churned customers. What the cohort decay took: last month's base plus this month's wins,
    /// less what is actually still here. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("churned_customers")] public decimal? ChurnedCustomers { get; set; }

    /// <summary>
    /// Revenue-share rate. Fees charged as a share of revenue, for the lines running this month.
    /// Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("revenue_fee_rate")] public decimal? RevenueFeeRate { get; set; }

    /// <summary>
    /// New customers. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("new_customers")] public long? NewCustomers { get; set; }

    /// <summary>
    /// Revenue-share fees. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("revenue_fees")] public decimal? RevenueFees { get; set; }

    /// <summary>
    /// Hiring one-off costs. Equipment and onboarding for the roles STARTING this month. Set by the
    /// runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("hiring_one_off")] public decimal? HiringOneOff { get; set; }

    /// <summary>
    /// Payroll-share rate. Fees charged as a share of gross payroll — Berufsgenossenschaft and the
    /// like. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("payroll_fee_rate")] public decimal? PayrollFeeRate { get; set; }

    /// <summary>
    /// One-off costs (by plan month). One-offs placed by plan month rather than by calendar date.
    /// Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("one_off_scheduled")] public decimal? OneOffScheduled { get; set; }

    /// <summary>
    /// One-off costs. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("one_off_total")] public decimal? OneOffTotal { get; set; }

    /// <summary>
    /// Payroll-share fees. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("payroll_fees")] public decimal? PayrollFees { get; set; }

    /// <summary>
    /// AI paid to the provider. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("ai_cost")] public decimal? AiCost { get; set; }

    /// <summary>
    /// Services (recurring). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("other_recurring")] public decimal? OtherRecurring { get; set; }

    /// <summary>
    /// Services (dated one-offs). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("other_one_off_dated")] public decimal? OtherOneOffDated { get; set; }

    /// <summary>
    /// Services (by plan month). In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("other_one_off_scheduled")] public decimal? OtherOneOffScheduled { get; set; }

    /// <summary>
    /// Services & other revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("other_revenue")] public decimal? OtherRevenue { get; set; }

    /// <summary>
    /// Total revenue. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("revenue")] public decimal? Revenue { get; set; }

    /// <summary>
    /// ARR run-rate. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("arr_run_rate")] public decimal? ArrRunRate { get; set; }

    /// <summary>
    /// Net cash movement. Cash equals revenue in this version — the workbook's own simplification.
    /// Annual prepayment is a later slice. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("net_cash_movement")] public decimal? NetCashMovement { get; set; }

    /// <summary>
    /// Cash at end. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cash_end")] public decimal? CashEnd { get; set; }

    /// <summary>
    /// Recurring MRR. Every live cohort at its own age. The number no single expression can reach,
    /// which is what the cohort grid is for. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("recurring_mrr")] public decimal? RecurringMrr { get; set; }

    /// <summary>
    /// Setup & services. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("services_revenue")] public decimal? ServicesRevenue { get; set; }

    /// <summary>
    /// Enterprise MRR (manual). Negotiated deal by deal, so there is no per-customer price to
    /// derive it from. Typed, like the workbook's own column. In EUR.
    /// </summary>
    [JsonPropertyName("enterprise_mrr")] public decimal? EnterpriseMrr { get; set; }

    /// <summary>
    /// AI billed to customers. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("ai_charge")] public decimal? AiCharge { get; set; }

    /// <summary>
    /// AI margin. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("ai_margin")] public decimal? AiMargin { get; set; }

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
