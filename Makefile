webapi:
	$(if $(PROJECT),,$(error PROJECT is required. Usage: make webapi PROJECT=YourProjectName))
	dotnet new webapi -n $(PROJECT) --no-openapi -controllers

classlib:
	$(if $(PROJECT),,$(error PROJECT is required. Usage: make classlib PROJECT=YourProjectName))
	dotnet new razorclasslib -n $(PROJECT)