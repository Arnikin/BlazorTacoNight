# BlazorTacoNight
Klaar om weggeblazord te worden?


## Exercise 1 - Create your first Blazor WebAssembly app

1. Create a folder called TacoBlazor
2. Open a command prompt and change directory to TacoBlazor
3. Create a new blazer project

`dotnet new blazorwasm -o MyFirstBlazorApp --no-https --hosted 
`cd MyFirstBlazorApp

3. Run the project
dotnet run

## Exercise 2 - Databinding
1. Open the SRA solution in the Exercise 2 folder
2. Open ExperimentTable in the Components folder of the Client project
3. To display the name of the experiment, fill the binding statement inside the foreach loop
@experiment.Name
4. Then create a Title parameter inside the @code block where it states
[Parameter]
public string Title { get; set; }
5. Show the parameter Title by putting a binding to the Title property inside the h3 tag on the top of the page.
@Title
6. Open the file Pages\Index.razor and show the ExperimentTable by adding
<ExperimentTable Title="Experiments" />
7. Press F5 to run the project

## Exercise 3 - Use components
1. Open the SRA solution in the Exercise3 folder
2. Create a component called Plate.razor in the Components folder by right click on Components, then select add, Razor component
3. Add a parameter called PlateModel inside the @code section
    [Parameter]
    public PlateModel PlateModel {get;set;}

4. Create a card that displays the title in the card header
    <div class="card">
        <div class="card-header">
            @(PlateModel.Title)
        </div>
        <div class="card-body">
		Body
        </div>
    </div>
5. Put the Plate component on the Plates.razor component on the designated spot, be sure to bind the plate instance to the Plate component 
<Plate PlateModel="@plate" />
6. Press F5 to run the solution

## Exercise 4 - Create a modal dialogue
1. Open the SRA solution in the Exercise4 folder
2. In Program.cs add the following line after the rootComponents.Add

builder.Services.AddBlazoredModal();

3. Open Plates.Razor in the Components folder and add the following statement after the IPlateViewModel injection

@inject IModalService modal

4. Add the following parameter to the @code section

[CascadingParameter] public IModalService Modal { get; set; }

5. Add the following method to the @code section

        private void Add()
        {
            var parameters = new ModalParameters();
            parameters.Add("ExperimentId", Guid.Empty);

            modal.Show<ExperimentDetail>("Add experiment", parameters);
        }

6. Add a button where it says "Put Add button here"

            <button type="button" class="btn btn-primary" @onclick="() => Add()">Add experiment</button>

7. Press F5 to run the solution
