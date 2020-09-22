# BlazorTacoNight
Klaar om weggeblazord te worden?


## Exercise 1 - Create your first Blazor WebAssembly app

1. Create a folder called TacoBlazor
2. Open a command prompt and change directory to TacoBlazor
3. Create a new blazer project

```cmd
dotnet new blazorwasm -o MyFirstBlazorApp --no-https --hosted
cd MyFirstBlazorApp
```
3. Run the project
```cmd
dotnet run
```

## Exercise 2 - Databinding
1. Open the SRA solution in the Exercise 2 folder
2. Open ExperimentTable in the Components folder of the Client project
3. To display the name of the experiment, fill the binding statement inside the foreach loop
```razor
	@experiment.Name
```
4. Then create a Title parameter inside the @code block where it states

```csharp
	[Parameter]
	public string Title { get; set; }
```
5. Show the parameter Title by putting a binding to the Title property inside the h3 tag on the top of the page.
```razor
	@Title
```

6. Open the file Pages\Index.razor and show the ExperimentTable by adding
```razor
<ExperimentTable Title="Experiments" />
```

7. Press F5 to run the project

## Exercise 3 - Use components
1. Open the SRA solution in the Exercise3 folder
2. Create a component called Plate.razor in the Components folder by right click on Components, then select add, Razor component
3. Add a parameter called PlateModel inside the @code section
```csharp
    [Parameter]
    public PlateModel PlateModel {get;set;}
```
4. Create a card that displays the title in the card header

```razor
    <div class="card">
        <div class="card-header">
            @(PlateModel.Title)
        </div>
        <div class="card-body">
		Body
        </div>
    </div>
```

5. Put the Plate component on the Plates.razor component on the designated spot, be sure to bind the plate instance to the Plate component 

```razor
<Plate PlateModel="@plate" />
```
6. Press F5 to run the solution

## Exercise 4 - Create a modal dialogue
1. Open the SRA solution in the Exercise4 folder
2. In Program.cs add the following line after the rootComponents.Add

```csharp
	builder.Services.AddBlazoredModal();
```

3. Open Plates.Razor in the Components folder and add the following statement after the IPlateViewModel injection

```razor
	@inject IModalService modal
```

4. Add the following parameter to the @code section

```razor
	[CascadingParameter] public IModalService Modal { get; set; }
```
5. Add the following method to the @code section

```csharp
        private void Add()
        {
            var parameters = new ModalParameters();
            parameters.Add("ExperimentId", Guid.Empty);

            modal.Show<ExperimentDetail>("Add experiment", parameters);
        }
```

6. Add a button where it says "Put Add button here"

```razor
	<button type="button" class="btn btn-primary" @onclick="() => Add()">Add experiment</button>
```
7. Press F5 to run the solution
