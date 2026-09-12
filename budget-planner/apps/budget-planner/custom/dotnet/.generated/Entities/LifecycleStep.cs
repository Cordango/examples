using System.Text.Json;
using System.Text.Json.Serialization;
using Cordango.Standalone.Records;

namespace BudgetPlanner.Entities;

/// <summary>
/// Lifecycle Step. One segment at one age: how big the customer is, what each plan would cost
/// them, and therefore what they pay. The cheapest plan wins, so this row is where a plan stops
/// being an assumption.
/// </summary>
public sealed class LifecycleStep : IRecord, IHasTrackingFields
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
    /// Segment. Holds the id of a segment record.
    /// </summary>
    [JsonPropertyName("segment")] public string Segment { get; set; } = "";

    /// <summary>
    /// Adoption point. Holds the id of a adoption_point record.
    /// </summary>
    [JsonPropertyName("point")] public string Point { get; set; } = "";

    /// <summary>
    /// Flex plan. Holds the id of a revenue_plan record.
    /// </summary>
    [JsonPropertyName("flex")] public string? Flex { get; set; }

    /// <summary>
    /// Starter plan. Holds the id of a revenue_plan record.
    /// </summary>
    [JsonPropertyName("starter")] public string? Starter { get; set; }

    /// <summary>
    /// Pro plan. Holds the id of a revenue_plan record.
    /// </summary>
    [JsonPropertyName("pro")] public string? Pro { get; set; }

    /// <summary>
    /// Advanced plan. Holds the id of a revenue_plan record.
    /// </summary>
    [JsonPropertyName("advanced")] public string? Advanced { get; set; }

    /// <summary>
    /// Age (months). Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("age")] public long? Age { get; set; }

    /// <summary>
    /// Active users (N). Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("active_users")] public decimal? ActiveUsers { get; set; }

    /// <summary>
    /// Shared apps. Never below one: a customer with no shared app is not a customer. Set by the
    /// runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("shared_apps")] public decimal? SharedApps { get; set; }

    /// <summary>
    /// Active users per app (A). Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("active_per_app")] public decimal? ActivePerApp { get; set; }

    /// <summary>
    /// Flex would cost. No base fee and no cap — every active user of every app is billed. In EUR.
    /// Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cost_flex")] public decimal? CostFlex { get; set; }

    /// <summary>
    /// Starter would cost. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cost_starter")] public decimal? CostStarter { get; set; }

    /// <summary>
    /// Pro would cost. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cost_pro")] public decimal? CostPro { get; set; }

    /// <summary>
    /// Advanced would cost. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cost_advanced")] public decimal? CostAdvanced { get; set; }

    /// <summary>
    /// MRR (cheapest plan). The customer is not asked to choose badly. Which plan won is readable
    /// from the four columns beside this one — naming it would need a conditional the expression
    /// language does not have. In EUR. Set by the runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("cheapest_plan_cost")] public decimal? CheapestPlanCost { get; set; }

    /// <summary>
    /// Surviving share of the cohort. pow(1 - churn, age - 1). Age one is a full cohort. Set by the
    /// runtime; not editable in a form.
    /// </summary>
    [JsonPropertyName("survival")] public decimal? Survival { get; set; }

    /// <summary>
    /// MRR per customer won. What one customer won at age zero is still paying at this age, churn
    /// already taken off. The grid multiplies this by the cohort size. In EUR. Set by the runtime;
    /// not editable in a form.
    /// </summary>
    [JsonPropertyName("revenue_per_new_customer")] public decimal? RevenuePerNewCustomer { get; set; }

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
