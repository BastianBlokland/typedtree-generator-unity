.PHONY: update-dependencies
default: update-dependencies

# --------------------------------------------------------------------------------------------------
# MakeFile used as a convient way for executing development utlitities.
# --------------------------------------------------------------------------------------------------

update-dependencies:
	./.ci/update-dependencies.sh
