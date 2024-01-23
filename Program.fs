module Main

open System.Threading.Tasks
open FSharp.Data.Sql
open FSharp.Data.Sql.Common


[<Literal>]
let private resolutionPath = __SOURCE_DIRECTORY__ + "/obj/db-libs/"

[<Literal>]
let private connectionString = "Host=localhost;Port=5433;Database=test;User ID=test;Password=test"

type postgres =
    SqlDataProvider<
        DatabaseVendor   = DatabaseProviderTypes.POSTGRESQL,
        ResolutionPath   = resolutionPath,
        ConnectionString = connectionString,
        UseOptionTypes   = NullableColumnType.OPTION
    >


let update (key : int) (value : int option) = task {
    let ctx = postgres.GetDataContext connectionString
    let row = ctx.Public.Test.Create()
    row.Id <- key
    row.Value <- value
    row.OnConflict <- OnConflict.Update
    do! ctx.SubmitUpdatesAsync()
}

[<EntryPoint>]
let main args =
    Task.WaitAll(task {
        try
            do! update 0 None
        with ex ->
            printfn "Exception: %A" ex
    })
    0
