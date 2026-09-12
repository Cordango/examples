using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Funding Round. Money coming in from investors within a scenario.
/// </summary>
public sealed class FundingRound : IRecord, IHasTrackingFields
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
    /// Round Status. One of: planned, in_conversation, term_sheet, closed, abandoned.
    /// </summary>
    [JsonPropertyName("round_stage")] public string? RoundStage { get; set; }

    /// <summary>
    /// Round Type. One of: bootstrap, grant, angel, pre_seed, seed, series_a, debt.
    /// </summary>
    [JsonPropertyName("round_type")] public string RoundType { get; set; } = "";

    /// <summary>
    /// Amount. In EUR.
    /// </summary>
    [JsonPropertyName("amount")] public decimal Amount { get; set; }

    [JsonPropertyName("expected_close")] public DateOnly ExpectedClose { get; set; }

    /// <summary>
    /// Pre-money Valuation. In EUR.
    /// </summary>
    [JsonPropertyName("pre_money_valuation")] public decimal? PreMoneyValuation { get; set; }

    /// <summary>
    /// Equity Given. Displayed with the unit '%'.
    /// </summary>
    [JsonPropertyName("equity_given")] public decimal? EquityGiven { get; set; }

    [JsonPropertyName("lead_investor")] public string? LeadInvestor { get; set; }

    [JsonPropertyName("investor_contact_email")] public string? InvestorContactEmail { get; set; }

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
