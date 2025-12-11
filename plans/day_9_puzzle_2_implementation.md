---
name: Day 9 Puzzle 2 Implementation 
overview: Implement Day 9 Puzzle 2 by finding the largest rectangle with red-corner opposite tiles whose interior is fully contained within the red+green region outlined by the given orthogonal polygon.
todos:
  - id: step1
    content: Parse red points and boundary edges
    status: completed
  - id: step2
    content: Compress coordinates for grid cells
    status: completed
  - id: step3
    content: Mark green region and build prefix sums
    status: completed
  - id: step4
    content: Validate red-corner rectangles for max area
    status: completed
  - id: step5
    content: Add puzzle2 tests and compute answers
    status: completed
---

# Day 9 Puzzle 2 Implementation using GPT-5.1 Codex Max model

## Overview
Compute the largest axis-aligned rectangle whose opposite corners are red tiles and whose entire area consists only of red or green tiles. Green tiles form the boundary lines between successive red points (wrapping around) and fill the interior of that orthogonal polygon.

## Steps

1) Parse red points and build polygon boundary
- Read ordered red tile coordinates (x,y) from input; close the loop implicitly.
- Store horizontal/vertical edges between consecutive points (including last→first) to describe the boundary.

2) Coordinate compression for tractable grid math
- Collect unique x and y breakpoints: every vertex coord and coord+1 to align to tile boundaries; sort and map to indices.
- Each compressed cell represents a block of full tiles with known width/height.

3) Mark green coverage on compressed grid
- Mark boundary cells crossed by each edge as green.
- For remaining cells, test a midpoint against the polygon with even–odd (ray casting); if inside, mark green.
- Build an area-weighted prefix sum (integral image) where each green cell contributes width*height.

4) Evaluate candidate rectangles with red corners
- For every pair of red tiles with differing x and y, compute rectangle bounds via compressed indices.
- Compute total tile count and compare to green area from the prefix; if equal, rectangle is fully red/green and valid.
- Track the maximum valid rectangle area and return it.

5) Tests and verification
- Add sample test expecting 24 for Puzzle 2.
- Add input-file test expecting the computed answer (1613305596).
