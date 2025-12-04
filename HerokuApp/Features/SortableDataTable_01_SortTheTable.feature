Feature: Sort table by column header on the Sortable Data Tables page

As a user
I want to click the header of a column
To sort table by that column

Background:
    Given I went on "https://the-internet.herokuapp.com/"
    And I followed the 'Sortable Data Tables' link

@smoke
Scenario: Clicking header sorts the table
    And I captured the 'Web Site' value for the row with email 'jsmith@gmail.com'
    When I click the 'First Name' column header
    Then the rows are sorted by the 'First Name' column
    And the captured value is still present in the 'Web Site' column
    And the 'Last Name' column contains the following values:
      | Last Name |
      | Smith     |
      | Bach      |
      | Doe       |
      | Conway    |