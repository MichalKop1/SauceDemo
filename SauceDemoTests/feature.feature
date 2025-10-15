#language: en
Feature: Validate if it is Sunday

@API
Scenario Outline: Check if it is Sunday

    Given I want to check if it is Sunday
    When I am at my computer
    Then Run the <script>
    And return the <value>
    

    Examples:
        | script    | value     | invalid_day |
        | cd C://   | Saturday  | Monday      |